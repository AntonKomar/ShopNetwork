using ShopNetwork.DAL;
using ShopNetwork.DAL.Models;
using ShopNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopNetwork.UI.Views;

namespace ShopNetwork.UI.ViewModels
{
    public class CatalogViewModel : ObservableObject 
    {
        private Product _product;
        private readonly ProductRepository _productRepository;
        private readonly GroupRepository _groupRepository;
        private readonly SubGroupRepository _subGroupRepository;
        private Group _selectedGroup;
        private SubGroup _selectedSubGroup;
        private CatalogView _catalogView;
        private ObservableCollection<SubGroup> _sub;
        private ObservableCollection<Product> _products;

        public CatalogViewModel(ProductRepository productRepository, GroupRepository groupRepository,
            SubGroupRepository subGroupRepository, CatalogView catalogView)
        {
            _productRepository = productRepository;
            _groupRepository = groupRepository;
            _subGroupRepository = subGroupRepository;
            _catalogView = catalogView;
            _products = new ObservableCollection<Product>(_productRepository.GetWithInclude(x => x.PictureID, x => x.Discount, x => x.SubGroup));
        }

        #region Properties

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products == value) return;

                _products = value;
                OnPropertyChanged("Products");
            }
        }

        public Product Product
        {
            get{ return _product; }
            set
            {
                if (_product == value) return;
                
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public ObservableCollection<Group> Groups => new ObservableCollection<Group>(_groupRepository.GetWithInclude(x => x.SubGroups));

        public Group SelectedGroup
        {
            private get { return _selectedGroup; }
            set
            {
                if (_selectedGroup == value) return;

                _selectedGroup = value;
                
               SubGroups = new ObservableCollection<SubGroup>(_selectedGroup.SubGroups);
               _catalogView.SubGroup.IsEnabled = true;

               OnPropertyChanged("SelectedGroup");
            }
        }

        public ObservableCollection<SubGroup> SubGroups
        {
            get
            {
                return _sub ?? _subGroupRepository.Get();
            }
            set
            {
                _sub = value;
                OnPropertyChanged("SubGroups");
            }
        }

        public SubGroup SelectedSubGroup
        {
            private get { return _selectedSubGroup; }
            set
            {
                if (_selectedSubGroup == value) return;

                _selectedSubGroup = value;

                Products = (SelectedSubGroup!=null) ? 
                    new ObservableCollection<Product>(_productRepository.GetWithInclude(x => x.PictureID, x => x.Discount, x => x.SubGroup)
                        .Where(x => x.SubGroup?.Name == _selectedSubGroup.Name)) : 
                    new ObservableCollection<Product>();

                OnPropertyChanged("SelectedSubGroup");
            }
        }

        #endregion



        #region Commands



        #endregion
    }
}
