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

namespace bilet5.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private ToDoList currentList = new ToDoList();
        public MainPage()
        {
            InitializeComponent();
            ToDoList.ItemsSource = DB.Context.ToDoList.ToList();
            DataContext = currentList;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleText.Text) || string.IsNullOrEmpty(DescText.Text))
            {
                MessageBox.Show("Введите данные");
                return;
            }

            if (currentList.Id == 0)
                DB.Context.ToDoList.Add(currentList);

            try
            {
                DB.Context.SaveChanges();
                ToDoList.ItemsSource = DB.Context.ToDoList.ToList();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            currentList = new ToDoList();
            DataContext = currentList;
        }

        private void ToDoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedList = ToDoList.SelectedItem as ToDoList;

            if (selectedList != null)
            {
                TitleText.Text = selectedList.Title;
                DescText.Text = selectedList.Description;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "";
            DescText.Text = "";

            var selectedList = ToDoList.SelectedItem as ToDoList;

            if (selectedList == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            if (MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    DB.Context.ToDoList.Remove(selectedList);
                    DB.Context.SaveChanges();
                    ToDoList.ItemsSource = DB.Context.ToDoList.ToList();
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedList = ToDoList.SelectedItem as ToDoList;

            if (selectedList == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            if (selectedList.Status == true)
                selectedList.Status = false;
            else
                selectedList.Status = true;

            DB.Context.SaveChanges();
            ToDoList.ItemsSource = DB.Context.ToDoList.ToList();
        }
    }
}
