using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ShopNetwork.DalPart;
using ShopNetwork.DalPart.Models;
using ShopNetwork.DalPart.Repositories;
using ShopNetwork.UI.Views;

namespace ShopNetwork.UI.ViewModels
{
    public class CatalogViewModel : ObservableObject 
    {
        private readonly ProductRepository _productRepository;
        private readonly GroupRepository _groupRepository;
        private readonly SubGroupRepository _subGroupRepository;
        private readonly CartRepository _cartRepository;
        private readonly AdminRepository _adminRepository;
        private readonly PersonRepository _personRepository;
        private Group _selectedGroup;
        private SubGroup _selectedSubGroup;
        private CatalogView _catalogView;
        private ObservableCollection<SubGroup> _sub;
        private ObservableCollection<Product> _products;
        private MainViewModel _mainViewModel;

        public CatalogViewModel(ProductRepository productRepository, GroupRepository groupRepository,
            SubGroupRepository subGroupRepository, CartRepository cartRepository, AdminRepository adminRepository,
            PersonRepository personRepository, CatalogView catalogView, MainViewModel mainViewModel)
        {
            _productRepository = productRepository;
            _groupRepository = groupRepository;
            _subGroupRepository = subGroupRepository;
            _cartRepository = cartRepository;
            _adminRepository = adminRepository;
            _personRepository = personRepository;
            _catalogView = catalogView;
            _products = new ObservableCollection<Product>(_productRepository.GetWithInclude(x => x.PictureID, x => x.Discount, x => x.SubGroup, x => x.Carts));
            _mainViewModel = mainViewModel;
        }

        #region Properties

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products == value) return;

                _products = value;
                OnPropertyChanged("Products");
            }
        }

        public ObservableCollection<Group> Groups => new ObservableCollection<Group>(_groupRepository.GetWithInclude(x => x.SubGroups));

        public Group SelectedGroup
        {
            private get { return _selectedGroup; }
            set
            {
                if (_selectedGroup == value) return;

                _selectedGroup = value;
                
               SubGroups = new ObservableCollection<SubGroup>(_selectedGroup.SubGroups);
               _catalogView.SubGroup.IsEnabled = true;

               OnPropertyChanged("SelectedGroup");
            }
        }

        public ObservableCollection<SubGroup> SubGroups
        {
            get
            {
                return _sub ?? _subGroupRepository.Get();
            }
            set
            {
                _sub = value;
                OnPropertyChanged("SubGroups");
            }
        }

        public SubGroup SelectedSubGroup
        {
            private get { return _selectedSubGroup; }
            set
            {
                if (_selectedSubGroup == value) return;

                _selectedSubGroup = value;

                Products = (SelectedSubGroup!=null) ? 
                    new ObservableCollection<Product>(_productRepository.GetWithInclude(x => x.PictureID, x => x.Discount, x => x.SubGroup)
                        .Where(x => x.SubGroup?.Name == _selectedSubGroup.Name)) : 
                    new ObservableCollection<Product>();

                OnPropertyChanged("SelectedSubGroup");
            }
        }

        #endregion

        #region Commands

        public void AddToCart(object sender)
        {
            //Check user
            if (_mainViewModel.SignInUser == default)
            {
                SignInDialogView s = new SignInDialogView();
                s.DataContext = _mainViewModel;
                s.ShowDialog();
            }
            else
            {
                //Get id of the product
                int prodId = 0;
                if (int.TryParse(((Button)sender).Tag.ToString(), out int res))
                {
                    prodId = res;
                }
                //Get the product by id
                Product product = _productRepository.Get().Where(x => x.ProdID == prodId).SingleOrDefault();
                
                //check if there is already such cart or no
                if (_cartRepository?.Get()
                        .Where(x => x.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"))
                        .SingleOrDefault() == null)
                {
                    //Create a new cart
                    Cart cart = new Cart();
                    cart.Date = DateTime.Now;
                    cart.Products = new List<Product>();

                    cart.Products.Add(product);

                    //If product didn't have any carts we declare it and add a cart
                    if(product?.Carts == null)
                        product.Carts = new List<Cart>();

                    //update context of user - admin or person
                    if (_mainViewModel.IsAdmin)
                        cart.AdminId = (Admin)_mainViewModel.SignInUser;
                    else
                        cart.PersonId = (Person)_mainViewModel.SignInUser;

                    product?.Carts.Add(cart);

                    //create new cart and update product context
                    _cartRepository.Create(cart);
                    _productRepository.Update(product);
                }
                else
                {
                    Cart cart = _cartRepository.GetWithInclude(x => x.Products)
                        .Where(x => x.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"))
                        .SingleOrDefault();

                    cart.Products.Add(product);

                    //If product didn't have any carts we declare it and add a cart
                    if (product?.Carts == null)
                        product.Carts = new List<Cart>();

                    //update context of user - admin or person
                    if (_mainViewModel.IsAdmin)
                        cart.AdminId = (Admin)_mainViewModel.SignInUser;
                    else
                        cart.PersonId = (Person)_mainViewModel.SignInUser;

                    product?.Carts.Add(cart);

                    //create new cart and update product context
                    _cartRepository.Update(cart);
                    _productRepository.Update(product);
                }
            }
        }

        #endregion
    }
}
