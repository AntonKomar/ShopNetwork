using ShopNetwork.DAL.Context;
using ShopNetwork.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Repositories
{
    public class SubGroupRepository : GenericRepository<SubGroup>
    {
        public SubGroupRepository(ShopNetworkContext context) : base(context)
        {

        }
    }
}
