using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string IMGUrl { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }

        [NotMapped]
        public IList<string> SelectedTags { get; set; }
        [NotMapped]
        public IList<SelectListItem> AllTags { get; set; }
        public Recipe()
        {
            SelectedTags = new List<string>();
            AllTags = new List<SelectListItem>();
        }

        [ForeignKey("Cook")]
        public int CookID { get; set; }
        public Cook Cook { get; set; }

        public ICollection<RecipeTags> RecipeTags { get; set; }

    }
}
