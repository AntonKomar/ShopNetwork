using ShopNetwork.DalPart.Context;
using ShopNetwork.DalPart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ShopNetwork.Interfaces
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IWcfPictureService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IWcfPictureService
    {
        [OperationContract]
        List<Picture> GetAllPictures();
    }
}
