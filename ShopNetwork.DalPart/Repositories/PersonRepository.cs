﻿using ShopNetwork.DalPart.Context;
using ShopNetwork.DalPart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Repositories
{
    public class PersonRepository : GenericRepository<Person>
    {
        public PersonRepository(ShopNetworkContext context) : base(context)
        {

        }
    }
}
