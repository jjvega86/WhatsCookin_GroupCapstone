using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class RecipeEdits
    {
        public int RecipeEditsId { get; set; }
        public int RecipeID { get; set; }
        public int CookId { get; set; }
        public string SuggestedEdit { get; set; }

    }
}
