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

namespace Nabrodov_WPF2.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorsPage.xaml
    /// </summary>
    public partial class AuthorsPage : Page
    {
        Authors _author = new Authors();
        public AuthorsPage()
        {
            InitializeComponent();
            AuthorsDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Authors.ToList();
            Form.DataContext = _author;
        }

        private void AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_author.FirstName))
                errors.AppendLine("Укажите имя автора");
            if (string.IsNullOrWhiteSpace(_author.LastName))
                errors.AppendLine("Укажите фамилию автора");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_author.Id == 0)
            {
                _author.Id = nabrodov_wpf2Entities1.GetContext().Authors.Max(s => s.Id) + 1;
                nabrodov_wpf2Entities1.GetContext().Authors.Add(_author);
            }

            try
            {
                nabrodov_wpf2Entities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                _author = new Authors();
                Form.DataContext = _author;
                AuthorsDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Authors.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            var AuthorForRemoving = AuthorsDataGrid.SelectedItems.Cast<Authors>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {AuthorForRemoving.Count()}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    nabrodov_wpf2Entities1.GetContext().Authors.RemoveRange(AuthorForRemoving);
                    nabrodov_wpf2Entities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    AuthorsDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Authors.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditAuthor_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Authors selectedAuthor = btn.DataContext as Authors;
            if (selectedAuthor != null)
            {
                _author = selectedAuthor;
                _author.Id = selectedAuthor.Id;

                Form.DataContext = _author;
            }
        }
    }
}
