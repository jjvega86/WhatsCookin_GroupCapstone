using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhatsCookinGroupCapstone.Contracts;
using WhatsCookinGroupCapstone.Data;
using WhatsCookinGroupCapstone.Models;

namespace WhatsCookinGroupCapstone.Controllers
{
    public class RecipesController : Controller
    {
        private IRepositoryWrapper _repo;

        public RecipesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Recipes
        public IActionResult Index()
        {
            List<Recipe> myRecipeList = new List<Recipe>();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedInCook = _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefault();
            var loggedInCookID = loggedInCook.CookId;
            myRecipeList = _repo.Recipe.FindByCondition(r => r.CookID == loggedInCookID).ToList();

            return View(myRecipeList);
        }

        // GET: Recipes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefault();

            if (recipe == null)
            {
                return NotFound();
            }


            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            Recipe recipe = new Recipe();
            {
                recipe.AllTags = GetTags();
            }
            return View(recipe);
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var loggedInCook = _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefault();
                var loggedInCookID = loggedInCook.CookId;
                recipe.CookID = loggedInCookID ;
                _repo.Recipe.Create(recipe);
                _repo.Save();

                //This is where the tags are bound to the recipe by the cook.
                var selectedRecipe = _repo.Recipe.FindByCondition(r => r.RecipeId == recipe.RecipeId).SingleOrDefault();
                var selectedRecipeId = selectedRecipe.RecipeId;

                foreach(string tags in recipe.SelectedTags)
                {
                    var selectedTags = _repo.Tags.FindByCondition(r => r.Name == tags).SingleOrDefault();

                    RecipeTags recipeTags = new RecipeTags();
                    recipeTags.RecipeId = selectedRecipeId;
                    recipeTags.TagsId = selectedTags.TagsId;
                    _repo.RecipeTags.Create(recipeTags);
                    _repo.Save();
                }




                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefault();
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RecipeId,RecipeName,IMGUrl,Ingredients,Description,Steps,CookID")] Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Recipe.Update(recipe);
                    _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _repo.Recipe
                .FindByCondition(m => m.RecipeId == id).FirstOrDefault();
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var recipe =  _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefault();
            //This code might cause a future problem
            _repo.Recipe.Delete(recipe);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            var foundRecip = _repo.Recipe.FindByCondition(r => r.RecipeId == id);
            if(foundRecip == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // GET: All Recipes
        //public IActionResult Search()
        //{
        //    var allRecipes = _repo.Recipe.FindAll();

        //    return View(allRecipes);
        //}

        //public IActionResult Saved(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var recipe = _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefault();
        //    if (recipe == null)
        //    {
        //        return NotFound();
        //    }
        //    return View();
        //}

        private IList<SelectListItem> GetTags()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Vegan", Value = "Vegan" },
                new SelectListItem { Text = "Paleo", Value = "Paleo" },
                new SelectListItem { Text = "Pescatarian", Value = "Pescatarian" },
                new SelectListItem { Text = "Nut-Free", Value = "Nut-Free" },
                new SelectListItem { Text = "Dairy", Value = "Dairy" }
            };

        }

        // GET: Recipes/Review/5
        public IActionResult Review(int id)
        {
            //validate passed in RecipeId
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                Reviews review = new Reviews();
                review.RecipeID = id;

                return View(review);

            }
            
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Review(Reviews review)
        {
            if (ModelState.IsValid)
            {
                _repo.Reviews.Create(review);
                _repo.Save();
               
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        //public IActionResult SeeReview(int id)
        //{

        //}


    }
}
