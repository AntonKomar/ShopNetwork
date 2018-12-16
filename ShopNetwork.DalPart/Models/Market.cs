using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Models
{
    public class Market
    {
        [Key]
        public int MarketID { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        [MaxLength(20)]
        public string Street { get; set; }
        public int Number { get; set; }

        public List<Product> Products { get; set; }
    }
}
