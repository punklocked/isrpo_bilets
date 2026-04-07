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

namespace nabrodov_shop.Pages.Admin_Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientEdit.xaml
    /// </summary>
    public partial class ClientEdit : Page
    {
        Users _user = new Users();
        public ClientEdit()
        {
            InitializeComponent();
            ClientDataGrid.ItemsSource = DB.Context.Users.ToList();
            Form.DataContext = _user;

            RoleCBox.ItemsSource = DB.Context.Roles.ToList();
        }

        private void SaveClient_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_user.Login))
                errors.AppendLine("Введите логин");
            if (string.IsNullOrWhiteSpace(_user.Password))
                errors.AppendLine("Введите пароль");
            if (string.IsNullOrWhiteSpace(_user.FIO))
                errors.AppendLine("Введите ФИО");
            if (string.IsNullOrWhiteSpace(_user.Phone))
                errors.AppendLine("Введите номер телефона");
            if (_user.Roles == null)
                errors.AppendLine("Выберите роль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_user.UserId == 0)
            {
                _user.UserId = DB.Context.Users.Max(s => s.UserId) + 1;
                DB.Context.Users.Add(_user);
            }

            try
            {
                DB.Context.SaveChanges();
                MessageBox.Show("Информация сохранена");
                _user = new Users();
                Form.DataContext = _user;
                ClientDataGrid.ItemsSource = DB.Context.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            var UserForRemoving = ClientDataGrid.SelectedItems.Cast<Users>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {UserForRemoving.Count()}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DB.Context.Users.RemoveRange(UserForRemoving);
                    DB.Context.SaveChanges();
                    MessageBox.Show("Данные удалены");

                    ClientDataGrid.ItemsSource = DB.Context.Users.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Users selectedUser = btn.DataContext as Users;
            if (selectedUser != null)
            {
                _user = selectedUser;
                _user.UserId = selectedUser.UserId;

                Form.DataContext = _user;
            }
        }
    }
}
