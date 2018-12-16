using ShopNetwork.DalPart.Context;
using ShopNetwork.DalPart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ShopNetworkContext context) : base(context)
        {

        }
    }
}
