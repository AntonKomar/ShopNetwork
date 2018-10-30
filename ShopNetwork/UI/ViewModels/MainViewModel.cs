using ShopNetwork.DAL;
using ShopNetwork.UI.Views;
using System;
using System.Collections.Generic;
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

        private RelayCommand _closeCommand;
        private RelayCommand _minimizeCommand;
        private RelayCommand _maximizeCommand;
        private RelayCommand _restoreCommand;
        private RelayCommand _listViewMenu;
        private RelayCommand _windowLoaded;
        private RelayCommand _signInCommand;

        #region Properties
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
                return _windowLoaded ?? (_windowLoaded = new RelayCommand(obj =>
                    {
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
                        RegistryDialogView dialogBox = new RegistryDialogView();
                        dialogBox.ShowDialog();
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
