using ShopNetwork.DAL;
using ShopNetwork.DAL.Models;
using ShopNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.UI.ViewModels
{
    public class CatalogViewModel : ObservableObject 
    {
        private Product _product;

        public ObservableCollection<Product> Products { get; set; }

        public CatalogViewModel(ProductRepository productRepository)
        {
            Products = productRepository.Get(); 
        }

        #region Properties

        public Product Product
        {
            get{ return _product; }
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        #endregion

        
        private RelayCommand getProductsOfSubroupCommand;

        #region Commands

        

        #endregion
    }
}
