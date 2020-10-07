using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class CookTag
    {
        [Key, Column(Order = 1)]
        public int TagsId { get; set; }
        public Tags Tag { get; set; }

        
        [Key, Column(Order = 2)]
        public int CookId { get; set; }
        public Cook Cook { get; set; }
    }
}
