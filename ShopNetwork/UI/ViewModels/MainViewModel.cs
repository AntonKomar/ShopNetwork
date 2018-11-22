using ShopNetwork.DAL;
using ShopNetwork.DAL.Context;
using ShopNetwork.DAL.Models;
using ShopNetwork.DAL.Repositories;
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
using System.Windows.Threading;

namespace ShopNetwork.UI.ViewModels
{
    public class MainViewModel : ObservableObject 
    {

        private MainWindow _mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        private string _currentDate;
        private string _time;
        private GroupRepository groupRepository;
        private SubGroupRepository subGroupRepository;
        private PersonRepository personRepository;
        private ShopNetworkContext shopNetworkContext;
        private User signInUser;

        public EventHandler CloseHandler;

        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<SubGroup> SubGroups { get; set; }

        public MainViewModel(ShopNetworkContext dbCont)
        {
            shopNetworkContext = dbCont;
            subGroupRepository = new SubGroupRepository(dbCont);
            groupRepository = new GroupRepository(dbCont);
            personRepository = new PersonRepository(dbCont);
            Groups = groupRepository.Get();
            SubGroups = subGroupRepository.Get();
        }

        private RelayCommand closeCommand;
        private RelayCommand minimizeCommand;
        private RelayCommand maximizeCommand;
        private RelayCommand restoreCommand;
        private RelayCommand listViewMenu;
        private RelayCommand windowLoaded;
        private RelayCommand signInCommand;
        private RelayCommand signInConfirmCommand;
        private RelayCommand signUpCommand;
        private RelayCommand signUpConfirmCommand;
        private RelayCommand signOutCommand;
        private RelayCommand signOutConfirmCommand;
        private RelayCommand getGroupsCommand;
        private RelayCommand getSubgroupsCommand;


        #region Properties

        public User SignInUser
        {
            get { return signInUser; }
            set
            {
                signInUser = value;
                OnPropertyChanged("signInUser");
            }
        }

        public IList<Gender> Genders
        {
            get
            {
                return Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            }
        }

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

        #region Commands

        public RelayCommand WindowLoaded
        {
            get
            {
                return windowLoaded ?? (windowLoaded = new RelayCommand(obj =>
                    {
                        _mainWindow.ContentArea.Content = new NewsView();
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
                return signInCommand ??
                    (signInCommand = new RelayCommand(obj =>
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
                return signInConfirmCommand ?? (signInConfirmCommand = new RelayCommand(obj =>
                {
                    if (SignInUser == default)
                    {
                        SignInDialogView signInView = Application.Current.Windows.OfType<SignInDialogView>().FirstOrDefault();
                        User user = (from c in personRepository.Get()
                                     where c.Email == signInView.email.Text
                                     select c).Single();

                        if (user != null && signInView.password.Text == user.Password)
                        {
                            if (user is Admin)
                            {

                            }
                            else
                            {
                                signInView.Close();
                                SignInUser = user;
                                _mainWindow.user.Content = user.Name;
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
                return signUpCommand ??
                    (signUpCommand = new RelayCommand(obj =>
                    {
                        SignUpDialogView dialogBox = new SignUpDialogView
                        {
                            DataContext = this
                        };
                        dialogBox.ShowDialog();
                    }));
            }
        }

        public RelayCommand SignUpConfirmCommand
        {
            get
            {
                return signUpConfirmCommand ?? (signUpConfirmCommand = new RelayCommand(obj =>
                {
                    SignUpDialogView signUpView = Application.Current.Windows.OfType<SignUpDialogView>().FirstOrDefault();
                    Person person = new Person
                    {
                        Name = signUpView.name.Text,
                        LastName = signUpView.lastName.Text,
                        Gender = (Gender)signUpView.gender.SelectedValue,
                        City = signUpView.city.Text,
                        Birth = (DateTime)signUpView.birth.SelectedDate,
                        DateReg = DateTime.Now,
                        Email = signUpView.email.Text,
                        Nickname = signUpView.nick.Text,
                        Phone = signUpView.phone.Text,
                        Password = signUpView.password.Password
                    };
                    personRepository.Create(person);
                    signUpView.Close();
                }));
            }
        }

        public RelayCommand SignOutCommand
        {
            get
            {
                return signOutCommand ??
                    (signOutCommand = new RelayCommand(obj =>
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
                return signOutConfirmCommand ??
                    (signOutConfirmCommand = new RelayCommand(obj =>
                    {
                        if (SignInUser != default)
                        {
                            _mainWindow.user.Content = "Guest";
                            _mainWindow.signOut.Visibility = Visibility.Collapsed;
                            _mainWindow.signIn.Visibility = Visibility.Visible;
                            _mainWindow.signUp.Visibility = Visibility.Visible;
                            SignOutDialog signOutView = Application.Current.Windows.OfType<SignOutDialog>().FirstOrDefault();
                            signOutView.Close();

                            if (SignInUser is Admin)
                            {
                                SignInUser = default;

                            }
                            else
                            {
                                SignInUser = default;
                            }
                        }
                    }));
            }
        }

        public RelayCommand GetGroupsCommand
        {
            get
            {
                return getGroupsCommand ??
                    (getGroupsCommand = new RelayCommand(obj =>
                    {
                        Groups.Select(x => x.Name);
                    }));
            }
        }

        public RelayCommand GetSubGroupsCommand
        {
            get
            {
                return getGroupsCommand ??
                    (getGroupsCommand = new RelayCommand(obj =>
                    {
                        SubGroups.Select(x => x.Name);
                    }));
            }
        }

        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                    (closeCommand = new RelayCommand(obj =>
                    {
                        Application.Current.Shutdown();
                    }));
            }
        }

        public RelayCommand MinimizeCommand
        {
            get
            {
                return minimizeCommand ?? 
                    (minimizeCommand = new RelayCommand(obj =>
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Minimized;
                    }));
            }
        }

        public RelayCommand MaximizeCommand
        {
            get
            {
                return maximizeCommand ??
                    (maximizeCommand = new RelayCommand(obj =>
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
                return restoreCommand ??
                    (restoreCommand = new RelayCommand(obj =>
                {
                    _mainWindow.WindowState = WindowState.Normal;
                    _mainWindow.RestoreButton.Visibility = Visibility.Collapsed;
                    _mainWindow.MaximizeButton.Visibility = Visibility.Visible;
                }));
            }
        }

        public RelayCommand ListViewMenu
        {
            get
            {
                return listViewMenu ?? 
                    (listViewMenu = new RelayCommand(obj =>
                {
                    switch ((obj as ListViewItem).Name)
                    {
                        case "News":
                            _mainWindow.ContentArea.Content = new NewsView();
                            break;
                        case "Catalog":
                            _mainWindow.ContentArea.Content = new CatalogView();
                            break;
                        case "Cart":
                            _mainWindow.ContentArea.Content = new CartView();
                            break;
                        case "Stores":
                            _mainWindow.ContentArea.Content = new OurStoresView();
                            break;
                        case "About":
                            _mainWindow.ContentArea.Content = new AboutUsView();
                            break;
                        default:
                            break;
                    }
                }));
            }
        }
        #endregion
    }
}
