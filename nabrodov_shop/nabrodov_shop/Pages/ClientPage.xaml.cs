using nabrodov_shop.Pages.Client_Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nabrodov_shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        Users currentUser = new Users();
        Products _product = new Products();
        ObservableCollection<CartItem> cart = new ObservableCollection<CartItem>();

        public ClientPage(Users selectedUser)
        {
            InitializeComponent();
            currentUser = selectedUser;
            ProductDataGrid.ItemsSource = DB.Context.Products.ToList();
            Form.DataContext = _product;

            var categories = DB.Context.Categories.ToList();

            categories.Insert(0, new Categories
            {
                CategoryId = 0,
                CategoryName = "Все категории"
            });

            CategoryCBox.ItemsSource = categories;
            CategoryCBox.SelectedIndex = 0;

            CartListView.ItemsSource = cart;

            DataContext = selectedUser;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Products selectedProduct = button.CommandParameter as Products;

            var existingItem = cart.FirstOrDefault(p => p.ProductName == selectedProduct.ProductName);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                CartListView.Items.Refresh();
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = selectedProduct.ProductId,
                    ProductName = selectedProduct.ProductName,
                    Price = selectedProduct.Price,
                    Quantity = 1
                });
            }

            UpdateTotalPrice();
        }

        private void SetFilter_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryCBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите категорию");
                return;
            }

            int categoryId = (int)CategoryCBox.SelectedValue;

            if (categoryId == 0)
                ProductDataGrid.ItemsSource = DB.Context.Products.ToList();
            else
                ProductDataGrid.ItemsSource = DB.Context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста");
                return;
            }

            var order = new Orders
            {
                UserId = currentUser.UserId,
                OrderDate = DateTime.Now,
                TotalPrice = cart.Sum(item => item.Total),
                Status = "Оформлен"
            };

            DB.Context.Orders.Add(order);
            DB.Context.SaveChanges();

            foreach (var item in cart)
            {
                var orderItem = new OrderItems
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };

                DB.Context.OrderItems.Add(orderItem);
            }

            DB.Context.SaveChanges();
            MessageBox.Show("Заказ успешно оформлен");
            cart.Clear();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = CartListView.SelectedItems.Cast<CartItem>().ToList();

            foreach (var item in selectedItems)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    cart.Remove(item);
            }

            UpdateTotalPrice();
        }

        private void ClearCart_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите очистить корзину?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                cart.Clear();
                UpdateTotalPrice();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        public void UpdateTotalPrice()
        {
            decimal total = cart.Sum(item => item.Total);
            TotalPrice.Text = $"Итого: {total}";
        }
    }
}
