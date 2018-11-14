using ShopNetwork.DAL.Context;
using ShopNetwork.DAL.Repositories;
using ShopNetwork.UI.ViewModels;
using System.Windows;

namespace ShopNetwork.UI.Views
{
    /// <summary>
    /// Interaction logic for SignUpDialogView.xaml
    /// </summary>
    public partial class SignUpDialogView : Window
    {
        public SignUpDialogView()
        {
            InitializeComponent();
            ShopNetworkContext dbCont = new ShopNetworkContext();
            PersonRepository personRepository = new PersonRepository(dbCont);
            DataContext = new SignUpViewModel(personRepository, this);
        }
    }
}
