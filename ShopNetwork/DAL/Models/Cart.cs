using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public DateTime Date { get; set; }

        public List<Product> Products { get; set; }
    }
}
