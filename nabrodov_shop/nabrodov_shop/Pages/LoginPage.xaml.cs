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

namespace nabrodov_shop.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Users _currentUser = new Users();
        public LoginPage()
        {
            InitializeComponent();
            DataContext = _currentUser;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            var existingUser = DB.Context.Users.FirstOrDefault(u => u.Login == _currentUser.Login && u.Password == _currentUser.Password);

            if (existingUser == null)
                errors.AppendLine("Неверный логин или пароль");
                
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            MessageBox.Show("Добро пожаловать " + existingUser.Login, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

            if (existingUser.RoleId == 1 || existingUser.RoleId == 2)
                NavigationService.Navigate(new AdminPage(selectedUser: existingUser));
            else
                NavigationService.Navigate(new ClientPage(selectedUser: existingUser));
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}
