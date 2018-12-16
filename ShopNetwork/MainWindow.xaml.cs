using ShopNetwork.DalPart.Context;
using ShopNetwork.DalPart.Models;
using ShopNetwork.Interfaces;
using ShopNetwork.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
            InitializeComponent();
            

            ChannelFactory<IWcfPictureService> channelFactory = new
                ChannelFactory<IWcfPictureService>("PictureService");

            IWcfPictureService proxy = channelFactory.CreateChannel();

            List<Picture> pictures = proxy.GetAllPictures();

            DataContext = new MainViewModel(dbCont);
        } 
    }
    
}
