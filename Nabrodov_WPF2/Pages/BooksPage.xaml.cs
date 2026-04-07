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
    /// Логика взаимодействия для BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        Books _book = new Books();
        public BooksPage()
        {
            InitializeComponent();
            BooksDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Books.ToList();
            Form.DataContext = _book;

            AuthorsCBox.ItemsSource = nabrodov_wpf2Entities1.GetContext().Authors.ToList();
            GenresCBox.ItemsSource = nabrodov_wpf2Entities1.GetContext().Genres.ToList();
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_book.Title))
                errors.AppendLine("Укажите название книги");
            if (_book.PublicationYear > DateTime.Today)
                errors.AppendLine("Укажите корректную дату публикации");
            if (_book.Authors == null)
                errors.AppendLine("Выберите автора");
            if (_book.Genres == null)
                errors.AppendLine("Выберите жанр");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_book.Id == 0)
            {
                _book.Id = nabrodov_wpf2Entities1.GetContext().Books.Max(s => s.Id) + 1;
                nabrodov_wpf2Entities1.GetContext().Books.Add(_book);
            }

            try
            {
                nabrodov_wpf2Entities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                _book = new Books();
                Form.DataContext = _book;
                BooksDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Books.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            var BookForRemoving = BooksDataGrid.SelectedItems.Cast<Books>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {BookForRemoving.Count()}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    nabrodov_wpf2Entities1.GetContext().Books.RemoveRange(BookForRemoving);
                    nabrodov_wpf2Entities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    BooksDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Books.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Books selectedBook = btn.DataContext as Books;
            if (selectedBook != null)
            {
                _book = selectedBook;
                _book.Id = selectedBook.Id;

                Form.DataContext = _book;
            }
        }
    }
}
