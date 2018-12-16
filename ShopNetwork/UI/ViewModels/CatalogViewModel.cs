using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopNetwork.DalPart;
using ShopNetwork.DalPart.Models;
using ShopNetwork.DalPart.Repositories;
using ShopNetwork.UI.Views;

namespace ShopNetwork.UI.ViewModels
{
    public class CatalogViewModel : ObservableObject 
    {
        private Product _product;
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


        private RelayCommand _addToCartCommand;

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

        public Product Product
        {
            get{ return _product; }
            set
            {
                if (_product == value) return;
                
                _product = value;
                OnPropertyChanged("Product");
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
            if (_mainViewModel.SignInUser == default)
            {
                SignInDialogView s = new SignInDialogView();
                s.DataContext = _mainViewModel;
                s.ShowDialog();
            }
            else
            {
                if (_cartRepository?.Get()
                        .Where(x => x.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"))
                        .SingleOrDefault() == null)
                {
                    Cart cart = new Cart();
                    cart.Date = DateTime.Now;

                    if(cart.Products == null)
                        cart.Products = new List<Product>();
                    //string ProdId = 
                    //Product product = _productRepository.Get().Where(x => x.ProdID == int.TryParse(sender,out int res));
                    cart.Products.Add(Product);

                    if(_mainViewModel.SignInUser.Carts == null)
                        _mainViewModel.SignInUser.Carts = new List<Cart>();

                    _mainViewModel.SignInUser.Carts.Add(cart);

                    if(Product?.Carts == null)
                        Product.Carts = new List<Cart>();

                    Product?.Carts.Add(cart);

                    if (_mainViewModel.IsAdmin)
                        _adminRepository.Update((Admin) _mainViewModel.SignInUser);
                    else
                        _personRepository.Update((Person) _mainViewModel.SignInUser);

                    _cartRepository.Create(cart);
                    _productRepository.Update(Product);
                }
                else
                {
                    Cart cart = _cartRepository.GetWithInclude(x => x.Products)
                        .Where(x => x.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"))
                        .SingleOrDefault();
                    cart.Products.Add(Product);

                    _cartRepository.Update(cart);
                }
            }
        }

        public RelayCommand AddToCartCommand
        {
            get
            {
                return _addToCartCommand ??
                       (_addToCartCommand = new RelayCommand(obj =>
                       {
                           if (_mainViewModel.SignInUser == default)
                           {
                               new SignInDialogView().ShowDialog();
                           }
                           else
                           {
                               if (_cartRepository.Get()
                                       .Where(x => x.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"))
                                       .SingleOrDefault() == null)
                               {
                                   Cart cart = new Cart();
                                   cart.Date = DateTime.Now;
                                   cart.Products.Add(Product);
                                   _mainViewModel.SignInUser.Carts.Add(cart);
                                   Product.Carts.Add(cart);

                                   if (_mainViewModel.IsAdmin)
                                       _adminRepository.Update((Admin)_mainViewModel.SignInUser);
                                   else
                                       _personRepository.Update((Person)_mainViewModel.SignInUser);

                                   _cartRepository.Create(cart);
                                   _productRepository.Update(Product);
                               }
                               else
                               {
                                   Cart cart = _cartRepository.GetWithInclude(x => x.Products)
                                       .Where(x => x.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"))
                                       .SingleOrDefault();
                                   cart.Products.Add(Product);

                                   _cartRepository.Update(cart);
                               }
                           }
                       }));
            }
        }

        #endregion
    }
}
