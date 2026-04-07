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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private User _currentUser = new User();
        public RegisterPage()
        {
            InitializeComponent();
            DataContext = _currentUser;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUser.fio))
                errors.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(_currentUser.login))
                errors.AppendLine("Укажите Login");
            if (string.IsNullOrWhiteSpace(_currentUser.password))
                errors.AppendLine("Укажите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUser.id == 0)
            {
                _currentUser.admin = false;
                DB.Context.User.Add(_currentUser);
            }

            try
            {
                DB.Context.SaveChanges();
                MessageBox.Show("Информация сохранена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new LKPage(DataContext as User));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Auto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
