using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Followers
    {
        [ForeignKey ("Cook")]
        public int CookID { get; set; }
        public Cook Cook { get; set; }

        [ForeignKey ("Cook")]
        public int FollowerID { get; set; }
        public Cook Follower { get; set; }
    }
}
