using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class RecipeTags
    {
        
        //[ForeignKey ("Recipe")]
        [Key, Column(Order = 1)]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        //[ForeignKey ("Tags")]
        [Key, Column(Order = 2)]
        public int TagsId { get; set; }
        public Tags Tags { get; set; }
    }
}
