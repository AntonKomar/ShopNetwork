using Microsoft.Win32;
using ShopNetwork.DAL.Models;
using ShopNetwork.DAL.Repositories;
using ShopNetwork.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ShopNetwork.UI.ViewModels
{
    public class SignUpViewModel
    {
        private PersonRepository personRepository;
        private PictureRepository pictureRepository;
        private AdminRepository adminRepository;
        private string imagePath;
        private string imageName;
        private bool isAdmin;
        private readonly string destPath = @"C:\Users\Anton\source\repos\ShopNetwork\ShopNetwork\DAL\Resources\Images\Users\";
        private SignUpDialogView signUpView;

        private RelayCommand signUpConfirmCommand;
        private RelayCommand chooseImageCommand;
        private RelayCommand removeImageCommand;

        public SignUpViewModel(PersonRepository personRepository, PictureRepository pictureRepository,
            AdminRepository adminRepository = default, bool isAdmin = false)
        {
            this.personRepository = personRepository;
            this.pictureRepository = pictureRepository;
            this.isAdmin = isAdmin;
            this.adminRepository = adminRepository;
            signUpView = Application.Current.Windows.OfType<SignUpDialogView>().FirstOrDefault();
        }

        public IList<Gender> Genders
        {
            get
            {
                return Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            }
        }

        public RelayCommand ChooseImageCommand
        {
            get
            {
                return chooseImageCommand ??
                    (chooseImageCommand = new RelayCommand(obj =>
                    {
                        var openDialog = new OpenFileDialog();
                        openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;" +
                        "*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";

                        if (openDialog.ShowDialog() == true)
                        {
                            imageName = openDialog.SafeFileName;
                            imagePath = openDialog.FileName;
                            signUpView.image.Source = new BitmapImage(new Uri(imagePath));
                            signUpView.remove.Visibility = Visibility.Visible;
                        }   
                    }));
            }
        }

        public RelayCommand RemoveImageCommand
        {
            get
            {
                return removeImageCommand ??
                    (removeImageCommand = new RelayCommand(obj =>
                    {
                        signUpView.image.Source = null;
                        imagePath = null;
                        imageName = null;
                        signUpView.remove.Visibility = Visibility.Hidden;
                    }));
            }
        }


        public RelayCommand SignUpConfirmCommand
        {
            get
            {
                return signUpConfirmCommand ?? (signUpConfirmCommand = new RelayCommand(obj =>
                {
                    User user = new Person();

                    if (imagePath != null)
                    {
                        System.IO.File.Copy(imagePath, destPath+imageName, true);
                        Picture picture = new Picture();
                        picture.FilePath = destPath+imageName;
                        pictureRepository.Create(picture);
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
                    if (isAdmin == true)
                        adminRepository.Create((Admin)user);
                    else
                        personRepository.Create((Person)user);

                    signUpView.Close();
                }));
            }
        }
    }
}
