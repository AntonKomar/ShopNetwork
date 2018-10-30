﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNetwork.DAL.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class User : ObservableObject
    {
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Image ImageID { get; set; }
        public DateTime Birth { get; set; }
        public DateTime DateReg { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        [Phone]
        public int Phone { get; set; }
        [EmailAddress]
        [MaxLength(30)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        public List<Cart> Carts { get; set; }
    }
}