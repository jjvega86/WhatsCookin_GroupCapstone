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
        public int Id { get; set; }
        //When creating the controller, set the follower user name equal to the cook user name using linq
        public string UserName { get; set; }

        //[ForeignKey ("Cook")]//cut if there are problems
        public int CookID { get; set; } //this is the cook that we are following, not the follower
        //public Cook Cook { get; set; }

        //[ForeignKey("Follower")]//ditto
        public int FollowerId { get; set; }
        //public Cook Follower { get; set; }
    }
}
