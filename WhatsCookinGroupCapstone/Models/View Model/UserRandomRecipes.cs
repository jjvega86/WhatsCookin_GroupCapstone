using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models.View_Model
{
    public class UserRandomRecipes
    {
        public List<Recipe> Recipes { get; set; } 
        public List<Cook> Cooks { get; set; }
        public List<Recipe> FeelinLucky { get; set; }


   
    }
}
