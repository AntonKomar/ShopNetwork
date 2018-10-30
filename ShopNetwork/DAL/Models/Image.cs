using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Models
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string FilePath { get; set; }
    }
}
