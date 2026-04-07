using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nabrodov_shop.Pages.Client_Cart
{
    public class CartItem : INotifyPropertyChanged
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(quantity));
                OnPropertyChanged(nameof(Total));
            }
        }
        public decimal Total => Price * Quantity;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string item)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item));
        }
    }
}
