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
        private UserRepository userRepository;
        private GroupRepository groupRepository;
        private SubGroupRepository subGroupRepository;

        public ObservableCollection<Product> Products { get; set; }

        public CatalogViewModel(UserRepository userRepository, ProductRepository productRepository, GroupRepository groupRepository,
            SubGroupRepository subGroupRepository, )
        {
           
            //this.Products = productRepository.Get() ; // Where(x => x.ProdID == category.Id).ToList();
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
    }
}
