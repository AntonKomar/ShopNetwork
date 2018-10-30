using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Models
{
    public class Admin : User
    {
        [Key]
        public int AdminID { get; set; }
        [Required]
        [MaxLength(20)]
        public int AdminName { get; set; }
    }
}
