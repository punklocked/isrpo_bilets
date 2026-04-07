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

namespace TourApp_Nabrodov
{
    /// <summary>
    /// Логика взаимодействия для Hotel.xaml
    /// </summary>
    public partial class Hotel : Page
    {
        public Hotel()
        {
            InitializeComponent();
            DGridHotel.ItemsSource = TourBaseEntities.GetContext().Hotels.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddHotel((sender as Button).DataContext as Hotels));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                TourBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotel.ItemsSource = TourBaseEntities.GetContext().Hotels.ToList();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddHotel(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var HotelForRemoving = DGridHotel.SelectedItems.Cast<Hotels>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {HotelForRemoving.Count()}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    TourBaseEntities.GetContext().Hotels.RemoveRange(HotelForRemoving);
                    TourBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridHotel.ItemsSource = TourBaseEntities.GetContext().Hotels.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
