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
        private AdminRepository adminRepository;

        private RelayCommand signInConfirmCommand;

        public SignInViewModel(MainViewModel main, MainWindow mainWindow, PersonRepository personRepository, AdminRepository adminRepository)
        {
            this.main = main;
            this.mainWindow = mainWindow;
            this.personRepository = personRepository;
            this.adminRepository = adminRepository;
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
                        User user = (from c in adminRepository.Get()
                                     where c.Email == signInView.email.Text
                                     select c).FirstOrDefault();

                        if (user != null && signInView.password.Password == user.Password)
                        {
                            signInView.Close();
                            main.SignInUser = user;
                            mainWindow.user.Content = "Admin";
                            mainWindow.signIn.Visibility = Visibility.Collapsed;
                            mainWindow.signUp.Visibility = Visibility.Collapsed;
                            mainWindow.signOut.Visibility = Visibility.Visible;
                            mainWindow.Admin.Visibility = Visibility.Visible;
                            mainWindow.addNews.Visibility = Visibility.Visible;
                            mainWindow.addProduct.Visibility = Visibility.Visible;
                            mainWindow.addStore.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            User user1 = (from c in personRepository.Get()
                                        where c.Email == signInView.email.Text
                                        select c).FirstOrDefault();
                            if (user1 != null && signInView.password.Password == user1.Password)
                            {
                                signInView.Close();
                                main.SignInUser = user1;
                                mainWindow.user.Content = user1.Name;
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
