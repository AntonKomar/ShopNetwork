using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Models
{
    public enum Gender
    {
        Male,
        Female
    }


    public class User 
    {
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Picture PictureID { get; set; }
        public DateTime Birth { get; set; }
        public DateTime DateReg { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        public string Phone { get; set; }
        
        [MaxLength(30)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
