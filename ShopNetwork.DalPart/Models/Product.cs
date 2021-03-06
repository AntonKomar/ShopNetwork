﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Models
{
    public enum Unity
    {
        item,
        pound,
        liter
    }
    
    public class Product
    {
        [Key]
        public int ProdID { get; set; } 
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Picture PictureID { get; set; }
        public int Quontity { get; set; }
        public Discount Discount { get; set; }
        public Unity UnityOfMeasurement { get; set; }
        public SubGroup SubGroup { get; set; }
        
        public List<Market> Markets { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
