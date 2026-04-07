using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ProductEdit.xaml
    /// </summary>
    public partial class ProductEdit : Page
    {
        Products _product = new Products();
        public ProductEdit()
        {
            InitializeComponent();
            ProductDataGrid.ItemsSource = DB.Context.Products.ToList();
            Form.DataContext = _product;

            CategoryCBox.ItemsSource = DB.Context.Categories.ToList();
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_product.ProductName))
                errors.AppendLine("Укажите название продукта");
            if (Convert.ToInt32(_product.Price) <= 0)
                errors.AppendLine("Введите действительное число");
            if (Convert.ToInt32(_product.Quantity) < 0)
                errors.AppendLine("Введите действительное число");
            if (_product.Categories == null)
                errors.AppendLine("Выберите категорию");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_product.ProductId == 0)
            {
                _product.ProductId = DB.Context.Products.Max(s => s.ProductId) + 1;
                DB.Context.Products.Add(_product);
            }

            try
            {
                DB.Context.SaveChanges();
                MessageBox.Show("Информация сохранена");
                _product = new Products();
                Form.DataContext = _product;
                ProductDataGrid.ItemsSource = DB.Context.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var ProductForRemoving = ProductDataGrid.SelectedItems.Cast<Products>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {ProductForRemoving.Count()}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DB.Context.Products.RemoveRange(ProductForRemoving);
                    DB.Context.SaveChanges();
                    MessageBox.Show("Данные удалены");

                    ProductDataGrid.ItemsSource = DB.Context.Products.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Products selectedProduct = btn.DataContext as Products;
            if (selectedProduct != null)
            {
                _product = selectedProduct;
                _product.ProductId = selectedProduct.ProductId;

                Form.DataContext = _product;
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NoSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
