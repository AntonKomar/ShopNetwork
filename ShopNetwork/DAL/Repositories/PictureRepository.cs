﻿using ShopNetwork.DAL.Context;
using ShopNetwork.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ShopNetwork.DAL.Repositories
{
    public class PictureRepository : GenericRepository<Picture>
    {
        public PictureRepository(ShopNetworkContext context) : base(context)
        {

        }
    }
}