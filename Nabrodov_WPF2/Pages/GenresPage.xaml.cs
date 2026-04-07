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
    /// Логика взаимодействия для GenresPage.xaml
    /// </summary>
    public partial class GenresPage : Page
    {
        Genres _genre = new Genres();
        public GenresPage()
        {
            InitializeComponent();
            GenresDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Genres.ToList();
            Form.DataContext = _genre;
        }

        private void AddGenre_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_genre.Name))
                errors.AppendLine("Укажите название жанра");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_genre.Id == 0)
            {
                _genre.Id = nabrodov_wpf2Entities1.GetContext().Genres.Max(s => s.Id) + 1;
                nabrodov_wpf2Entities1.GetContext().Genres.Add(_genre);
            }

            try
            {
                nabrodov_wpf2Entities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                _genre = new Genres();
                Form.DataContext = _genre;
                GenresDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Genres.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            var GenreForRemoving = GenresDataGrid.SelectedItems.Cast<Genres>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {GenreForRemoving.Count()}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    nabrodov_wpf2Entities1.GetContext().Genres.RemoveRange(GenreForRemoving);
                    nabrodov_wpf2Entities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    GenresDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Genres.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditGenre_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Genres selectedGenre = btn.DataContext as Genres;
            if (selectedGenre != null)
            {
                _genre = selectedGenre;
                _genre.Id = selectedGenre.Id;

                Form.DataContext = _genre;
            }
        }
    }
}
