using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Cook
    {
        public int CookId { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
