
using ShopNetwork.DAL.Context;
using ShopNetwork.DAL.Repositories;
using ShopNetwork.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopNetwork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ShopNetworkContext dbCont = new ShopNetworkContext();
            GroupRepository gR = new GroupRepository(dbCont);
            SubGroupRepository sG = new SubGroupRepository(dbCont);
            InitializeComponent();
            DataContext = new MainViewModel(gR, sG);
        }

        
    }
    
}
