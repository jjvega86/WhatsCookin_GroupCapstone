using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class RecipeTags
    {
        [ForeignKey ("Recipe")]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        [ForeignKey ("Tags")]
        public int TagID { get; set; }
        public Tags Tags { get; set; }
    }
}
