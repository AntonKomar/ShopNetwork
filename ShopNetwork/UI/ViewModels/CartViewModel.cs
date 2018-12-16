using ShopNetwork.DalPart;
using ShopNetwork.DalPart.Models;
using ShopNetwork.DalPart.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.UI.ViewModels
{
    public class CartViewModel:ObservableObject
    {
        private CartRepository _cartRepository;
        private MainViewModel _mainViewModel;
        private Cart _selectedCart;

        public CartViewModel(CartRepository cartRepository, MainViewModel mainViewModel)
        {
            _cartRepository = cartRepository;
            _mainViewModel = mainViewModel;
        }

        public ObservableCollection<Cart> Carts
        {
            get { return new ObservableCollection<Cart>(_mainViewModel.SignInUser.Carts); }
        }

        public Cart SelectedCart
        {
            get { return _selectedCart; }
            set
            {
                _selectedCart = value;
                OnPropertyChanged("SelectedCart");
            }
        }

        public ObservableCollection<Product> CartProducts
        {
            get
            {
                return (SelectedCart != null) ? new ObservableCollection<Product>(SelectedCart.Products)
                    : new ObservableCollection<Product>();
            }
        }
    }
}
