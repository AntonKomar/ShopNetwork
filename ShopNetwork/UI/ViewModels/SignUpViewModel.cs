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
        private string imagePath = null;
        private string imageName = null;
        private string destPath = @"C:\Users\Anton\source\repos\ShopNetwork\ShopNetwork\DAL\Resources\Images\Users\";
        private SignUpDialogView signUpView;

        private RelayCommand signUpConfirmCommand;
        private RelayCommand chooseImageCommand;
        private RelayCommand removeImageCommand;

        public SignUpViewModel(PersonRepository personRepository, PictureRepository pictureRepository)
        {
            this.personRepository = personRepository;
            this.pictureRepository = pictureRepository;
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
                        openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";

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
                    Person person = new Person();

                    if (imagePath != null)
                    {
                        System.IO.File.Copy(imagePath, destPath+imageName, true);
                        Picture picture = new Picture();
                        picture.FilePath = destPath+imageName;
                        pictureRepository.Create(picture);
                        person.PictureID = picture;
                    }

                    person.Name = signUpView.name.Text;
                    person.LastName = signUpView.lastName.Text;
                    person.Gender = (Gender)signUpView.gender.SelectedValue;
                    person.City = signUpView.city.Text;
                    person.Birth = (DateTime)signUpView.birth.SelectedDate;
                    person.DateReg = DateTime.Now;
                    person.Email = signUpView.email.Text;
                    person.Phone = signUpView.phone.Text;
                    person.Password = signUpView.password.Password;
                    personRepository.Create(person);
                    signUpView.Close();
                }));
            }
        }
    }
}
