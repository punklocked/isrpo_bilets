using Nabrodov_WPF2.Pages;
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

namespace Nabrodov_WPF2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new BooksPage());
        }

        private void GotoBooksPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BooksPage());
        }

        private void GotoAuthorsPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AuthorsPage());
        }

        private void GotoGenresPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GenresPage());
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SearchPage());
        }
    }
}
