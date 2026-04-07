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

namespace Nabrodov_UserApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        User _user = new User();
        public AdminPage(User selectedUser)
        {
            InitializeComponent();
            DataContext = selectedUser;
            UserGrid.ItemsSource = DB.Context.User.Where(u => u.id != 1).ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void ChangeRole_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            User selectedUser = btn.DataContext as User;
            if (selectedUser != null)
            {
                _user = selectedUser;
                _user.id = selectedUser.id;

                if (selectedUser.admin == true)
                    selectedUser.admin = false;
                else selectedUser.admin = true;

                DB.Context.SaveChanges();
                UserGrid.ItemsSource = DB.Context.User.Where(u => u.id != 1).ToList();
            }
        }
    }
}
