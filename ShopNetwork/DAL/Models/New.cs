using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Models
{
    public class New
    {
        [Key]
        public int NewID { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Picture PictureID { get; set; }

    }
}
