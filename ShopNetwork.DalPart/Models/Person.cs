﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Models
{
    public class Person : User
    {
        [Key]
        public int PersonID { get; set; }
    }
}
