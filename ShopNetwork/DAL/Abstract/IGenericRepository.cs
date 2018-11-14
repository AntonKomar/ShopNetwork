using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Abstract
{
    interface IGenericRepository<T> 
        where T : class
    {
        void Create(T item);
        T FindById(int id);
        ObservableCollection<T> Get();
        ObservableCollection<T> Get(Func<T, bool> predicate);
        void Remove(T item);
        void Update(T item);
    }
}
