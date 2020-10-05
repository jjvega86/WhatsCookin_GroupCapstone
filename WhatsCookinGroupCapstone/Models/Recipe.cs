using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public string IMGUrl { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }

        [ForeignKey("Cook")]
        public int CookID { get; set; }
        public Cook Cook { get; set; }
        
    }
}
