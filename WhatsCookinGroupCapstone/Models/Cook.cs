using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Cook
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }

        [ForeignKey("User")]
        public int CookID { get; set; }
        public User User { get; set; }
    }
}
