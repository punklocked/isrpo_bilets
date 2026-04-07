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
    /// Логика взаимодействия для TourPage.xaml
    /// </summary>
    public partial class TourPage : Page
    {
        public TourPage()
        {
            InitializeComponent();
            var allTypes = TourBaseEntities.GetContext().TourType.ToList();
            allTypes.Insert(0, new TourType
            {
                Name = "Все типы"
            });
            CheckActual.IsChecked = false;
            ComboType.SelectedIndex = 0;
            ComboType.ItemsSource = allTypes;



            var currentTours = TourBaseEntities.GetContext().Tours.ToList();
            LViewTour.ItemsSource = currentTours;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTour();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTour();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTour();
        }
        private void UpdateTour()
        {
            var currentTours = TourBaseEntities.GetContext().Tours.ToList();
            if (ComboType.SelectedIndex > 0)
            {
                currentTours = currentTours.Where(p => p.TourType.Contains(ComboType.SelectedItem as TourType)).ToList();
            }
            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(Search.Text.ToLower())).ToList();

            if (CheckActual.IsChecked.Value)
            {
                currentTours = currentTours.Where(p => p.IsActual).ToList();
            }
            LViewTour.ItemsSource = currentTours.OrderBy(p => p.TicketCount).ToList();
        }
    }
}
