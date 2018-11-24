using ShopNetwork.DAL.Models;
using ShopNetwork.DAL.Repositories;
using ShopNetwork.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopNetwork.UI.ViewModels
{
    public class SignInViewModel
    {
        private MainViewModel main;
        private MainWindow mainWindow;
        private PersonRepository personRepository;

        private RelayCommand signInConfirmCommand;

        public SignInViewModel(MainViewModel main, MainWindow mainWindow, PersonRepository personRepository)
        {
            this.main = main;
            this.mainWindow = mainWindow;
            this.personRepository = personRepository;
        }

        public RelayCommand SignInConfirmCommand
        {
            get
            {
                return signInConfirmCommand ?? (signInConfirmCommand = new RelayCommand(obj =>
                {
                    if (main.SignInUser == default)
                    {
                        SignInDialogView signInView = Application.Current.Windows.OfType<SignInDialogView>().FirstOrDefault();
                        User user = (from c in personRepository.Get()
                                     where c.Email == signInView.email.Text
                                     select c).Single();

                        if (user != null && signInView.password.Password == user.Password)
                        {
                            if (user is Admin)
                            {

                            }
                            else
                            {
                                signInView.Close();
                                main.SignInUser = user;
                                mainWindow.user.Content = user.Name;
                                mainWindow.signIn.Visibility = Visibility.Collapsed;
                                mainWindow.signUp.Visibility = Visibility.Collapsed;
                                mainWindow.signOut.Visibility = Visibility.Visible;
                            }
                        }
                    }

                }));
            }
        }
    }
}
