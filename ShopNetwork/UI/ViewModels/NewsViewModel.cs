using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopNetwork.DAL;
using ShopNetwork.DAL.Context;
using ShopNetwork.DAL.Models;
using ShopNetwork.DAL.Repositories;

namespace ShopNetwork.UI.ViewModels
{
    public class NewsViewModel:ObservableObject
    {
        private NewsRepository _newsRepository;

        public NewsViewModel(NewsRepository news)
        {
            _newsRepository = news;
        }

        public ObservableCollection<New> News
        {
            get { return new ObservableCollection<New>(_newsRepository.GetWithInclude(x => x.PictureID)); }
        }
    }
}
