﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Models
{
    public class SubGroup
    {
        [Key]
        public int SubGroupID { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        
    }
}
