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
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        List<Authors> allAuthors = nabrodov_wpf2Entities1.GetContext().Authors.ToList();
        List<Genres> allGenres = nabrodov_wpf2Entities1.GetContext().Genres.ToList();
        public SearchPage()
        {
            InitializeComponent();
            SearchDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Books.ToList();
        }

        private void SearchBooks_Click(object sender, RoutedEventArgs e)
        {
            SearchDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Books.ToList();

            string search = SearchBox.Text.ToLower();

            if (search == "")
            {
                SearchDataGrid.ItemsSource = nabrodov_wpf2Entities1.GetContext().Books.ToList();
                return;
            }

            List<Books> result = new List<Books>();

            string type = (SearchType.SelectedItem as ComboBoxItem).Content.ToString();

            foreach (Books book in SearchDataGrid.ItemsSource)
            {
                if (type == "Название")
                {
                    if (book.Title.ToLower().Contains(search))
                        result.Add(book);
                }

                if (type == "Автор")
                {
                    foreach (Authors author in allAuthors)
                    {
                        if (author.Id == book.AuthorId)
                        {
                            if (author.LastName.ToLower().Contains(search))
                                result.Add(book);
                        }
                    }
                }

                if (type == "Год выпуска")
                {
                    if (book.PublicationYear.ToString().Contains(search))
                        result.Add(book);
                }

                if (type == "Жанр")
                {
                    foreach (Genres genre in allGenres)
                    {
                        if (genre.Id == book.GenreId)
                        {
                            if (genre.Name.ToLower().Contains(search))
                                result.Add(book);
                        }
                    }
                }
            }
            SearchDataGrid.ItemsSource = result;
        }
    }
}
