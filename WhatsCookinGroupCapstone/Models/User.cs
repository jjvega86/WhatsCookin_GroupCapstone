using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class User
    {
       
        public int UserID { get; set; }
        

        [ForeignKey("Cook")]
        public int CookID { get; set; }
        public Cook Cook { get; set; }


    }
}
