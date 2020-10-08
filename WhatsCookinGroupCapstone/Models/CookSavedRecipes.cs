using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class CookSavedRecipes
    {
        [Key, Column(Order = 1)]
        public int CookId { get; set; }
        public Cook Cook { get; set; }


        [Key, Column(Order = 2)]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
