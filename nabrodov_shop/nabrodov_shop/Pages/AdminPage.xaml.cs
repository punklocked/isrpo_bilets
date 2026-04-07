using System;
using nabrodov_shop.Pages.Admin_Pages;
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

namespace nabrodov_shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage(Users selectedUser)
        {
            InitializeComponent();
            if (selectedUser.RoleId == 2)
            {
                ProductBtn.IsEnabled = false;
                ClientBtn.IsEnabled = false;
                AdminFrame.Navigate(new OrderEdit());
            }
            else
                AdminFrame.Navigate(new ProductEdit());
            DataContext = selectedUser;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new ProductEdit());
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new ClientEdit());
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Navigate(new OrderEdit());
        }
    }
}
