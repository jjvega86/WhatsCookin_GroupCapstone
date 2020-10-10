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
        [Key]
        public int CookSavedRecipesId { get; set; }
        public int CookId { get; set; }
        //MarkedAsCooked
        public int RecipeId { get; set; }
        [NotMapped]
        public List<Recipe> AllRecipes { get; set; }

    }
}
