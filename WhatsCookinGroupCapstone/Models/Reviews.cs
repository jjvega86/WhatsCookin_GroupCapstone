using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models
{
    public class Reviews
    {
        //All subject to change depending on the API we will be adding later
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string ReviewForRecipe { get; set; }
        public bool Validation { get; set; }

        public int RecipeID { get; set; }

    }
}
