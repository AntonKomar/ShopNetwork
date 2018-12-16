using Microsoft.Win32;
using ShopNetwork.DalPart;
using ShopNetwork.DalPart.Context;
using ShopNetwork.DalPart.Models;
using ShopNetwork.DalPart.Repositories;
using ShopNetwork.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ShopNetwork.UI.ViewModels
{
    public class MainViewModel : ObservableObject 
    {
        #region Fields
        private readonly MainWindow _mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        private string _currentDate;
        private string _time;
        private readonly GroupRepository _groupRepository;
        private readonly SubGroupRepository _subGroupRepository;
        private readonly PersonRepository _personRepository;
        private readonly PictureRepository _pictureRepository;
        private readonly AdminRepository _adminRepository;
        private readonly DiscountRepository _discountRepository;
        private readonly ProductRepository _productRepository;
        private readonly NewsRepository _newsRepository;
        private readonly CartRepository _cartRepository;  
        
        private Group _selectedGroup;
        private ObservableCollection<SubGroup> _sub;

        private string _imagePath;
        private string _imageName;
        private const string DestPath = @"C:\Users\Anton\source\repos\ShopNetwork\ShopNetwork\DalPart\Resources\Images\";
        private bool _isDiscount;
        private User _signInUser;

        #region Relay command
        private RelayCommand _closeCommand;
        private RelayCommand _minimizeCommand;
        private RelayCommand _maximizeCommand;
        private RelayCommand _restoreCommand;
        private RelayCommand _listViewMenu;
        private RelayCommand _mouseDownCommand;
        private RelayCommand _windowLoaded;

        private RelayCommand _signInCommand;
        private RelayCommand _signInConfirmCommand;
        private RelayCommand _signUpCommand;

        private RelayCommand _signUpConfirmCommand;
        private RelayCommand _chooseImageCommand;
        private RelayCommand _removeImageCommand;

        private RelayCommand _signOutCommand;
        private RelayCommand _signOutConfirmCommand;

        private RelayCommand _addProductCommand;
        private RelayCommand _addProductConfirmCommand;
        private RelayCommand _chooseImageProductCommand;
        private RelayCommand _removeImageProductCommand;
        private RelayCommand _addNewsCommand;
        private RelayCommand _addNewsConfirmCommand;
        private RelayCommand _removeImageNewsCommand;
        private RelayCommand _chooseImageNewsCommand;
        private RelayCommand _addStoreCommand;
        private RelayCommand _addStoreConfirmCommand;
        #endregion


        #endregion

        public MainViewModel(ShopNetworkContext dbCont)
        {
            _subGroupRepository = new SubGroupRepository(dbCont);
            _groupRepository = new GroupRepository(dbCont);
            _personRepository = new PersonRepository(dbCont);
            _pictureRepository = new PictureRepository(dbCont);
            _adminRepository = new AdminRepository(dbCont);
            _discountRepository = new DiscountRepository(dbCont);
            _productRepository = new ProductRepository(dbCont);
            _newsRepository = new NewsRepository(dbCont);
        }

        #region Properties

        public bool IsAdmin { get; set; }

        public ObservableCollection<Group> Groups => new ObservableCollection<Group>(_groupRepository.GetWithInclude(x => x.SubGroups));

        public Group SelectedGroup
        {
            private get { return _selectedGroup; }
            set
            {
                if(_selectedGroup == value) return;
                
                _selectedGroup = value;

                AddProductDialog addProductDialog = Application.Current.Windows.OfType<AddProductDialog>().FirstOrDefault();
                SubGroups = new ObservableCollection<SubGroup>(_selectedGroup.SubGroups);
                addProductDialog.subGroup.IsEnabled = true;

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

        public bool IsDiscount
        {
            get { return _isDiscount; }
            set
            {
                if (_isDiscount == value) return;

                _isDiscount = value;
                AddProductDialog addProductDialog = Application.Current.Windows.OfType<AddProductDialog>().FirstOrDefault();
                if (_isDiscount)
                {
                    addProductDialog.dateStart.IsEnabled = true;
                    addProductDialog.dateEnd.IsEnabled = true;
                    addProductDialog.newPrice.IsEnabled = true;
                }
                else
                {
                    addProductDialog.dateStart.IsEnabled = false;
                    addProductDialog.dateEnd.IsEnabled = false;
                    addProductDialog.newPrice.IsEnabled = false;
                }
            }
        }

        public User SignInUser
        {
            get => _signInUser;
            set
            {
                _signInUser = value;
                OnPropertyChanged("SignInUser");
            }
        }

        public IList<Gender> Genders => Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

        public IList<Unity> Units => Enum.GetValues(typeof(Unity)).Cast<Unity>().ToList();

        public string CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }
        #endregion

        /// <summary>
        /// Choose image with OpenFileDialog.
        /// Return if image was chosen or no.
        /// </summary>
        /// <returns></returns>
        private bool ChooseImage()
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;" +
                                "*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            _imageName = null;
            _imagePath = null;
            if(openDialog.ShowDialog() == true)
            {
                _imageName = openDialog.SafeFileName;
                _imagePath = openDialog.FileName;
                return true;
            }
            return false;
        }

        #region Commands

        #region MainWindow
        public RelayCommand WindowLoaded
        {
            get
            {
                return _windowLoaded ?? (_windowLoaded = new RelayCommand(obj =>
                    {
                        NewsView news = new NewsView();
                        news.DataContext = new NewsViewModel(_newsRepository);
                        _mainWindow.ContentArea.Content = news;

                        DispatcherTimer dispatcherTimer = new DispatcherTimer();
                        dispatcherTimer.Tick += new EventHandler((object sender,EventArgs e) => 
                        {
                            Time = DateTime.Now.ToString("HH:mm");
                            CurrentDate = DateTime.Now.ToString("ddd dd");
                        });
                        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                        dispatcherTimer.Start();
                    }));
            }
        }

        
        public RelayCommand SignInCommand
        {
            get
            {
                return _signInCommand ??
                    (_signInCommand = new RelayCommand(obj =>
                    {
                        SignInDialogView dialogBox = new SignInDialogView
                        {
                            DataContext = this
                        };
                        dialogBox.ShowDialog();
                    }));
            }
        }

        public RelayCommand SignInConfirmCommand
        {
            get
            {
                return _signInConfirmCommand ?? (_signInConfirmCommand = new RelayCommand(obj =>
                {
                    if (SignInUser == default)
                    {
                        SignInDialogView signInView = Application.Current.Windows.OfType<SignInDialogView>().FirstOrDefault();
                        User user = (from c in _adminRepository.GetWithInclude(x => x.Carts)
                                     where c.Email == signInView.email.Text
                                     select c).FirstOrDefault();

                        if (user != null && signInView.password.Password == user.Password)
                        {
                            signInView.Close();
                            SignInUser = user;
                            _mainWindow.user.Content = "Admin";
                            IsAdmin = true;
                            _mainWindow.signIn.Visibility = Visibility.Collapsed;
                            _mainWindow.signUp.Visibility = Visibility.Collapsed;
                            _mainWindow.signOut.Visibility = Visibility.Visible;
                            _mainWindow.Admin.Visibility = Visibility.Visible;
                            _mainWindow.addNews.Visibility = Visibility.Visible;
                            _mainWindow.addProduct.Visibility = Visibility.Visible;
                            _mainWindow.addStore.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            User user1 = (from c in _personRepository.GetWithInclude(x=>x.Carts)
                                          where c.Email == signInView.email.Text
                                          select c).FirstOrDefault();
                            if (user1 != null && signInView.password.Password == user1.Password)
                            {
                                signInView.Close();
                                SignInUser = user1;
                                _mainWindow.user.Content = user1.Name;
                                IsAdmin = false;
                                _mainWindow.signIn.Visibility = Visibility.Collapsed;
                                _mainWindow.signUp.Visibility = Visibility.Collapsed;
                                _mainWindow.signOut.Visibility = Visibility.Visible;
                            }
                        }
                    }

                }));
            }
        }

        public RelayCommand SignUpCommand
        {
            get
            {
                return _signUpCommand ??
                    (_signUpCommand = new RelayCommand(obj =>
                    {
                        SignUpDialogView dialogBox = new SignUpDialogView
                        {
                            DataContext = this
                        };
                        dialogBox.ShowDialog();
                    }));
            }
        }

        public RelayCommand ChooseImageSignUpCommand
        {
            get
            {
                return _chooseImageCommand ??
                    (_chooseImageCommand = new RelayCommand(obj =>
                    {
                        SignUpDialogView signUpView = Application.Current.Windows.OfType<SignUpDialogView>().FirstOrDefault();
                        
                        if (ChooseImage())
                        {
                            signUpView.image.Source = new BitmapImage(new Uri(_imagePath));
                            signUpView.remove.Visibility = Visibility.Visible;
                        }
                    }));
            }
        }

        public RelayCommand RemoveImageSignUpCommand
        {
            get
            {
                return _removeImageCommand ??
                    (_removeImageCommand = new RelayCommand(obj =>
                    {
                        SignUpDialogView signUpView = Application.Current.Windows.OfType<SignUpDialogView>().FirstOrDefault();
                        signUpView.image.Source = null;
                        _imagePath = null;
                        _imageName = null;
                        signUpView.remove.Visibility = Visibility.Hidden;
                    }));
            }
        }


        public RelayCommand SignUpConfirmCommand
        {
            get
            {
                return _signUpConfirmCommand ?? (_signUpConfirmCommand = new RelayCommand(obj =>
                {
                    SignUpDialogView signUpView = Application.Current.Windows.OfType<SignUpDialogView>().FirstOrDefault();
                    User user = new User();

                    if (_imagePath != null)
                    {
                        System.IO.File.Copy(_imagePath, DestPath+ @"Users\" + _imageName, true);
                        Picture picture = new Picture();
                        picture.FilePath = DestPath + _imageName;
                        _pictureRepository.Create(picture);
                        user.PictureID = picture;
                    }

                    user.Name = signUpView.name.Text;
                    user.LastName = signUpView.lastName.Text;
                    user.Gender = (Gender)signUpView.gender.SelectedValue;
                    user.City = signUpView.city.Text;
                    user.Birth = (DateTime)signUpView.birth.SelectedDate;
                    user.DateReg = DateTime.Now;
                    user.Email = signUpView.email.Text;
                    user.Phone = signUpView.phone.Text;
                    user.Password = signUpView.password.Password;
                    if (IsAdmin == true)
                        _adminRepository.Create((Admin)user);
                    else
                        _personRepository.Create((Person)user);

                    signUpView.Close();
                }));
            }
        }

        public RelayCommand SignOutCommand
        {
            get
            {
                return _signOutCommand ??
                    (_signOutCommand = new RelayCommand(obj =>
                    {
                        SignOutDialog dialogBox = new SignOutDialog { DataContext = this };
                        dialogBox.ShowDialog();
                    }));
            }
        }

        public RelayCommand SignOutConfirmCommand
        {
            get
            {
                return _signOutConfirmCommand ??
                    (_signOutConfirmCommand = new RelayCommand(obj =>
                    {
                        if (SignInUser != default)
                        {
                            _mainWindow.user.Content = "Guest";
                            _mainWindow.signOut.Visibility = Visibility.Collapsed;
                            _mainWindow.signIn.Visibility = Visibility.Visible;
                            _mainWindow.signUp.Visibility = Visibility.Visible;
                            SignOutDialog signOutView = Application.Current.Windows.OfType<SignOutDialog>().FirstOrDefault();
                            signOutView.Close();

                            if (SignInUser.GetType() == typeof(Admin))
                            {
                                _mainWindow.Admin.Visibility = Visibility.Collapsed;
                                _mainWindow.addNews.Visibility = Visibility.Collapsed;
                                _mainWindow.addProduct.Visibility = Visibility.Collapsed;
                                _mainWindow.addStore.Visibility = Visibility.Collapsed;
                            }
                            SignInUser = default;
                            

                        }
                    }));
            }
        }

        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ??
                    (_closeCommand = new RelayCommand(obj =>
                    {
                        Application.Current.Shutdown();
                    }));
            }
        }

        public RelayCommand MinimizeCommand
        {
            get
            {
                return _minimizeCommand ??
                    (_minimizeCommand = new RelayCommand(obj =>
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Minimized;
                    }));
            }
        }

        public RelayCommand MaximizeCommand
        {
            get
            {
                return _maximizeCommand ??
                    (_maximizeCommand = new RelayCommand(obj =>
                    {
                        _mainWindow.WindowState = WindowState.Maximized;
                        _mainWindow.MaximizeButton.Visibility = Visibility.Collapsed;
                        _mainWindow.RestoreButton.Visibility = Visibility.Visible;
                    }));
            }
        }

        public RelayCommand RestoreCommand
        {
            get
            {
                return _restoreCommand ??
                    (_restoreCommand = new RelayCommand(obj =>
                    {
                        _mainWindow.WindowState = WindowState.Normal;
                        _mainWindow.RestoreButton.Visibility = Visibility.Collapsed;
                        _mainWindow.MaximizeButton.Visibility = Visibility.Visible;
                    }));
            }
        }

        public RelayCommand MouseDownCommand
        {
            get
            {
                return _mouseDownCommand ??
                       (_mouseDownCommand = new RelayCommand(obj =>
                       {
                           _mainWindow.DragMove();
                       }));
            }
        }

        public RelayCommand ListViewMenu
        {
            get
            {
                return _listViewMenu ??
                    (_listViewMenu = new RelayCommand(obj =>
                    {
                        switch ((obj as ListViewItem).Name)
                        {
                            case "News":
                                NewsView news = new NewsView();
                                news.DataContext = new NewsViewModel(_newsRepository);
                                _mainWindow.ContentArea.Content = news;
                                break;
                            case "Catalog":
                                CatalogViewModel vm = new CatalogViewModel(_productRepository, _groupRepository, _subGroupRepository,
                                    _cartRepository, _adminRepository, _personRepository, null, this);
                                CatalogView catalog = new CatalogView(vm);
                                catalog.DataContext = new CatalogViewModel(_productRepository,_groupRepository,_subGroupRepository,
                                    _cartRepository, _adminRepository, _personRepository, catalog, this);
                                _mainWindow.ContentArea.Content = catalog;
                                break;
                            case "Cart":
                                if (SignInUser != null)
                                {
                                    CartView cart = new CartView();
                                    cart.DataContext = new CartViewModel(_cartRepository, this);
                                    _mainWindow.ContentArea.Content = cart;
                                }
                                else
                                {
                                    SignInDialogView s = new SignInDialogView();
                                    s.DataContext = this;
                                    s.ShowDialog();
                                }
                                break;
                            case "Stores":
                                _mainWindow.ContentArea.Content = new OurStoresView();
                                break;
                            case "About":
                                _mainWindow.ContentArea.Content = new AboutUsView();
                                break;
                            case "Admin":
                                SignUpDialogView dialogBox = new SignUpDialogView
                                {
                                    DataContext = this
                                };
                                dialogBox.ShowDialog();
                                break;
                            default:
                                break;
                        }
                    }));
            }
        }

        #endregion

        #region ProductDialog

        public RelayCommand AddProductCommand
        {
            get
            {
                return _addProductCommand ??
                    (_addProductCommand = new RelayCommand(obj => 
                    {
                        AddProductDialog dialogBox = new AddProductDialog(){ DataContext = this };
                        dialogBox.ShowDialog();
                    }));
            }
        }

        public RelayCommand AddProductConfirmCommand
        {
            get
            {
                return _addProductConfirmCommand ??
                    (_addProductConfirmCommand = new RelayCommand(obj =>
                    {
                        AddProductDialog addProductDialog = Application.Current.Windows.OfType<AddProductDialog>().FirstOrDefault();
                        Product product = new Product();

                        if (_imagePath != null)
                        {
                            System.IO.File.Copy(_imagePath, DestPath+ @"Products\" + _imageName, true);
                            Picture picture = new Picture();
                            picture.FilePath = DestPath+ @"Products\"+ _imageName;
                            _pictureRepository.Create(picture);
                            product.PictureID = picture;
                        }

                        if (IsDiscount)
                        {
                            Discount discount = new Discount();
                            discount.DateStart = (DateTime)addProductDialog.dateStart.SelectedDate;
                            discount.DateEnd = (DateTime)addProductDialog.dateEnd.SelectedDate;
                            discount.Price = int.TryParse(addProductDialog.newPrice.Text, out int result) ? result : default;
                            _discountRepository.Create(discount);
                            product.Discount = discount;
                        }

                        product.Name = addProductDialog.name.Text;
                        product.Description = addProductDialog.descript.Text;
                        product.UnityOfMeasurement = (Unity) addProductDialog.unity.SelectedValue;
                        product.Price = int.TryParse(addProductDialog.price.Text, out int result1) ? result1 : default;
                        product.Quontity = int.TryParse(addProductDialog.quontity.Text, out int result2) ? result2 : default;
                        product.SubGroup = (SubGroup)addProductDialog.subGroup.SelectedValue;

                        _productRepository.Create(product);

                        addProductDialog.Close();
                    }));
            }
        }

        public RelayCommand ChooseImageProductCommand
        {
            get
            {
                return _chooseImageProductCommand ?? (_chooseImageProductCommand = new RelayCommand(obj =>
                {
                    AddProductDialog addProductDialog = Application.Current.Windows.OfType<AddProductDialog>().FirstOrDefault();
                    
                    if (ChooseImage())
                    {
                        addProductDialog.image.Source = new BitmapImage(new Uri(_imagePath));
                        addProductDialog.remove.Visibility = Visibility.Visible;
                    }
                }));
            }
        }

        public RelayCommand RemoveImageProductCommand
        {
            get
            {
                return _removeImageProductCommand ??
                       (_removeImageProductCommand = new RelayCommand(obj =>
                       {
                           AddProductDialog addProductDialog = Application.Current.Windows.OfType<AddProductDialog>().FirstOrDefault();
                           addProductDialog.image.Source = null;
                           _imagePath = null;
                           _imageName = null;
                           addProductDialog.remove.Visibility = Visibility.Hidden;
                       }));
            }
        }

        #endregion

        #region News Dialog

        public RelayCommand AddNewsCommand
        {
            get
            {
                return _addNewsCommand ??
                    (_addNewsCommand = new RelayCommand(obj =>
                    {
                        AddNewsDialog dialogBox = new AddNewsDialog() { DataContext = this };
                        dialogBox.ShowDialog();
                    }));
            }
        }

        public RelayCommand AddNewsConfirmCommand
        {
            get
            {
                return _addNewsConfirmCommand ??
                       (_addNewsConfirmCommand = new RelayCommand(obj =>
                       {
                           AddNewsDialog addNewsDialog = Application.Current.Windows.OfType<AddNewsDialog>().FirstOrDefault();
                           New news = new New();

                           if (_imagePath != null)
                           {
                               System.IO.File.Copy(_imagePath, DestPath + @"News\" + _imageName, true);
                               Picture picture = new Picture();
                               picture.FilePath = DestPath + @"News\" + _imageName;
                               _pictureRepository.Create(picture);
                               news.PictureID = picture;
                           }

                           news.Name = addNewsDialog?.name.Text;
                           news.Description = addNewsDialog?.descript.Text;
                           news.Date = DateTime.Now;

                           _newsRepository.Create(news);
                           
                           addNewsDialog.Close();
                       }));
            }
        }

        public RelayCommand ChooseImageNewsCommand
        {
            get
            {
                return _chooseImageNewsCommand ?? (_chooseImageNewsCommand = new RelayCommand(obj =>
                {
                    AddNewsDialog addNewsDialog = Application.Current.Windows.OfType<AddNewsDialog>().FirstOrDefault();

                    if (ChooseImage())
                    {
                        addNewsDialog.image.Source = new BitmapImage(new Uri(_imagePath));
                        addNewsDialog.remove.Visibility = Visibility.Visible;
                    }
                }));
            }
        }

        public RelayCommand RemoveImageNewsCommand
        {
            get
            {
                return _removeImageNewsCommand ??
                       (_removeImageProductCommand = new RelayCommand(obj =>
                       {
                           AddProductDialog addNewsDialog = Application.Current.Windows.OfType<AddProductDialog>().FirstOrDefault();
                           addNewsDialog.image.Source = null;
                           _imagePath = null;
                           _imageName = null;
                           addNewsDialog.remove.Visibility = Visibility.Hidden;
                       }));
            }
        }

        #endregion

        public RelayCommand AddStoreCommand
        {
            get
            {
                return _addStoreCommand ??
                    (_addStoreCommand = new RelayCommand(obj =>
                    {
                        AddStoreDialog dialogBox = new AddStoreDialog() { DataContext = this };
                        dialogBox.ShowDialog();
                    }));
            }
        }
        

        #endregion
    }
}
