using System;
using System.Collections.Generic;
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

namespace nabrodov_shop.Pages.Admin_Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Page
    {
        Orders _order = new Orders();
        public OrderEdit()
        {
            InitializeComponent();
            OrderDataGrid.ItemsSource = DB.Context.Orders.ToList();
            Form.DataContext = _order;
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Orders selectedOrder = btn.DataContext as Orders;
            if (selectedOrder != null)
            {
                SaveButton.IsEnabled = true;
                _order = selectedOrder;

                Form.DataContext = _order;
            }
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_order.Status))
                errors.AppendLine("Выберите статус заказа");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                SaveButton.IsEnabled = false;
                return;
            }

            try
            {
                DB.Context.SaveChanges();
                MessageBox.Show("Информация сохранена");
                _order = new Orders();
                Form.DataContext = _order;
                OrderDataGrid.ItemsSource = DB.Context.Orders.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            SaveButton.IsEnabled = false;
        }

        private void DeleteOrder_CLick(object sender, RoutedEventArgs e)
        {
            var OrderForRemoving = OrderDataGrid.SelectedItems.Cast<Orders>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {OrderForRemoving.Count()}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (var order in OrderForRemoving)
                    {
                        var items = DB.Context.OrderItems.Where(i => i.OrderId == order.OrderId).ToList();
                        DB.Context.OrderItems.RemoveRange(items);
                    }

                    DB.Context.Orders.RemoveRange(OrderForRemoving);
                    DB.Context.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    OrderDataGrid.ItemsSource = DB.Context.Orders.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem == null)
                return;

            var selectedOrder = OrderDataGrid.SelectedItem as Orders;

            var items = DB.Context.OrderItems.Where(item => item.OrderId == selectedOrder.OrderId).Select(item => new
            {
                item.Products.ProductName,
                item.Price,
                item.Quantity,
                Total = item.Price * item.Quantity
            }).ToList();

            ItemsDataGrid.ItemsSource = items;

            TotalPrice.Text = "Итого: " + selectedOrder.TotalPrice.ToString();

            var user = DB.Context.Users.FirstOrDefault(u => u.UserId == selectedOrder.UserId);

            if (user != null)
                UserOrdered.Text = "Пользователь: " + user.Login;
        }
    }
}
