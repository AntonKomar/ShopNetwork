using ShopNetwork.DalPart.Context;
using ShopNetwork.DalPart.Models;
using ShopNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfPictureService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "WcfPictureService" в коде и файле конфигурации.
    public class WcfPictureService : IWcfPictureService
    {
        public List<Picture> GetAllPictures()
        {
            using (ShopNetworkContext _context = new ShopNetworkContext())
            {
                return _context.Pictures.AsNoTracking().ToList();
            }
        }
    }
}
