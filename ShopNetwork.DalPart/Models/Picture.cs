using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Models
{

    public class Picture
    {
        [Key]
        public int PictureID { get; set; }
        public string FilePath { get; set; }
    }
}
