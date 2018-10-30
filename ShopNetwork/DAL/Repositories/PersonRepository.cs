using ShopNetwork.DAL.Context;
using ShopNetwork.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Repositories
{
    public class PersonRepository : GenericRepository<Person>
    {
        public PersonRepository(ShopNetworkContext context) : base(context)
        {

        }
    }
}
