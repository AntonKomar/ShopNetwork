using ShopNetwork.DAL;
using ShopNetwork.DAL.Models;
using ShopNetwork.DAL.Repositories;
using ShopNetwork.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopNetwork.UI.ViewModels
{
    public class SignUpViewModel:ObservableObject
    {
        private PersonRepository personRepository;
        private SignUpDialogView signUpView;

        public SignUpViewModel(PersonRepository personRepository, SignUpDialogView signUpView)
        {
            this.personRepository = personRepository;
            this.signUpView = signUpView;
        }

        public IList<Gender> Genders
        {
            get
            {
                return Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            }
        }

        private RelayCommand signUpPersonCommand;
        
        public RelayCommand SignUpPersonCommand
        {
            get
            {
                return signUpPersonCommand ?? (signUpPersonCommand = new RelayCommand(obj =>
                {
                    Person person = new Person();
                    person.Name = signUpView.name.Text;
                    person.LastName = signUpView.lastName.Text;
                    person.Gender = (Gender)signUpView.gender.SelectedValue;
                    person.City = signUpView.city.Text;
                    person.Birth = (DateTime)signUpView.birth.SelectedDate;
                    person.DateReg = DateTime.Now;
                    person.Email = signUpView.email.Text;
                    person.Nickname = signUpView.nick.Text;
                    person.Phone = signUpView.phone.Text;
                    person.Password = signUpView.password.Password;
                    personRepository.Create(person);
                    signUpView.Close();
                    }));
            }
        }
    }
}
