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
            get
            {
                if (_mainViewModel.IsAdmin)
                {
                    return new ObservableCollection<Cart>(_cartRepository.GetWithInclude(x => x.Products));
                   //new ObservableCollection<Cart>(_cartRepository.Get().Where(x => x.AdminId.AdminID == ((Admin)_mainViewModel.SignInUser).AdminID));
                }
                else
                {
                    return new ObservableCollection<Cart>(_cartRepository.GetWithInclude(x => x.Products));
                    //new ObservableCollection<Cart>(_cartRepository.Get().Where(x => x.PersonId.PersonID == ((Person)_mainViewModel.SignInUser).PersonID));
                }
            }
        }

        public Cart SelectedCart
        {
            get { return _selectedCart; }
            set
            {
                if (_selectedCart == value) return;

                _selectedCart = value;
                OnPropertyChanged("SelectedCart");
                OnPropertyChanged("CartProducts");
            }
        }

        public ObservableCollection<Product> CartProducts
        {
            get
            {
                return (_selectedCart != null) ? new ObservableCollection<Product>(SelectedCart.Products)
                    : new ObservableCollection<Product>(new List<Product>());
            }
        }
    }
}
