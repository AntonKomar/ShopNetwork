using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DalPart.Models
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        public string Name { get; set; }

        public List<SubGroup> SubGroups { get; set; }
    }
}
