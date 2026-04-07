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
    /// Логика взаимодействия для AddHotel.xaml
    /// </summary>
    public partial class AddHotel : Page
    {
        public AddHotel(Hotels selectedHotel)
        {
            InitializeComponent();

            if (selectedHotel != null)
                _currentHotel = selectedHotel;

            DataContext = _currentHotel;
            ComboCountries.ItemsSource = TourBaseEntities.GetContext().Countries.ToList();
        }

        private Hotels _currentHotel = new Hotels();

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CountOfStar < 1 || _currentHotel.CountOfStar > 5)
                errors.AppendLine("Количество звезд - число от 1 до 5");
            if (_currentHotel.Countries == null)
                errors.AppendLine("Выберите страну");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel.Id == 0)
            {
                _currentHotel.Id = TourBaseEntities.GetContext().Hotels.Max(s => s.Id) + 1;
                TourBaseEntities.GetContext().Hotels.Add(_currentHotel);
            }


            try
            {
                TourBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            Manager.MainFrame.GoBack();
        }
    }
}
