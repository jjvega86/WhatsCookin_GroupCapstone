using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsCookinGroupCapstone.Models.View_Model
{
    public class UserRandomRecipes
    {
        public IEnumerable<Recipe> Recipes { get; set; }


        public void RandomizeRecipes()
        {
            Random random = new Random();
            //Recipe[] recipeArray;
            List<Recipe> randomRecipes = new List<Recipe>();
            for (int i = 0; i < 6; i++)
            {
                var recipeList = _repo.Recipe.Fin
                List<Recipe> recipeList = Recipes.ToList();
                int index = random.Next(recipeList.Count);
                randomRecipes.Add(recipeList[index]);


                recipeArray[i] = random.
            }
        }
    }
}
