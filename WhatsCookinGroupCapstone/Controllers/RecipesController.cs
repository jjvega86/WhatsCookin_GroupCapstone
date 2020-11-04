﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
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
        private readonly IRepositoryWrapper _repo;

        public RecipesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            List<Recipe> myRecipeList = new List<Recipe>();

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedInCook =  _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefault();
            var loggedInCookID = loggedInCook.CookId;
            myRecipeList = await _repo.Recipe.FindByCondition(r => r.CookID == loggedInCookID).ToListAsync();

            return View(myRecipeList);
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
        public async Task<IActionResult> Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var loggedInCook = await _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefaultAsync();
                var loggedInCookID = loggedInCook.CookId;
                recipe.CookID = loggedInCookID;
                recipe.CookName = loggedInCook.UserName;
                _repo.Recipe.Create(recipe);
                _repo.Save();

                //This is where the tags are bound to the recipe by the cook.
                var selectedRecipe = await _repo.Recipe.FindByCondition(r => r.RecipeId == recipe.RecipeId).SingleOrDefaultAsync();
                var selectedRecipeId = selectedRecipe.RecipeId;

                foreach(string tags in recipe.SelectedTags)
                {
                    var selectedTags = await _repo.Tags.FindByCondition(r => r.Name == tags).SingleOrDefaultAsync();

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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefaultAsync();

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _repo.Recipe
                .FindByCondition(m => m.RecipeId == id).FirstOrDefaultAsync();

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe =  await _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefaultAsync();
            //This code might cause a future problem
            _repo.Recipe.Delete(recipe);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            var foundRecip = _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefault();
            if(foundRecip == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

      //  GET: All Recipes
        public async Task<IActionResult> Search(string searchString)
        {
            var allRecipes = _repo.Recipe.FindAll();


          //  var AllRecipes = from r in _repo.Recipe.FindAll()
            //             select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                allRecipes = allRecipes.Where(a => a.RecipeName.Contains(searchString));
            }

            return View(await allRecipes.ToListAsync());
        }
        [HttpPost]
        public string Search(string searchString, bool notUsed)
        {
            return "From [HttpPost]Search: filter on " + searchString;
        }

        public async Task<IActionResult> CooksSaved()
        {
            CookSavedRecipes saveRecipe = new CookSavedRecipes()
            {
                AllRecipes = new List<Recipe>()
            };
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var foundCook = await _repo.Cook.FindByCondition(r => r.IdentityUserId == userId).SingleOrDefaultAsync();
         
            if (foundCook == null)
            {
                return NotFound();

            }
            
            var cookSavedRecipes = await _repo.CookSavedRecipes.FindByCondition(s => s.CookId == foundCook.CookId).ToListAsync();
           
            foreach (CookSavedRecipes savedRecipe in cookSavedRecipes)
            {
                var recipe = await _repo.Recipe.FindByCondition(r => r.RecipeId == savedRecipe.RecipeId).SingleOrDefaultAsync();
                saveRecipe.AllRecipes.Add(recipe);
            }
            return View(saveRecipe);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefaultAsync();

            if (recipe == null)
            {
                return NotFound();
            }


            return View(recipe);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Details(int id)
        {
            var recipe = _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefault();

            CookSavedRecipes saveRecipe = new CookSavedRecipes();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var foundCook = await _repo.Cook.FindByCondition(r => r.IdentityUserId == userId).SingleOrDefaultAsync();
            var foundRecipe = await _repo.Recipe.FindByCondition(r => r.RecipeId == recipe.RecipeId).FirstOrDefaultAsync();
            

            if (foundCook == null)
            {
                return NotFound();

            }
            saveRecipe.CookId = foundCook.CookId;
            saveRecipe.RecipeId = foundRecipe.RecipeId;

            //If they own it redirect them
            var cookOwnsThisRecipe = _repo.CookSavedRecipes.FindByCondition(s => s.CookId == foundRecipe.CookID).FirstOrDefault();
            if(cookOwnsThisRecipe != null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var alreadySaved1 = _repo.CookSavedRecipes.FindByCondition(s => s.RecipeId == foundRecipe.RecipeId).ToList();
                foreach (CookSavedRecipes recip in alreadySaved1)
                {
                    if (foundCook.CookId == recip.CookId)
                    {
                        return RedirectToAction(nameof(CooksSaved));
                    }
                }
            }
            catch
            {
                SaveToRepo(recipe, saveRecipe);
            }

            SaveToRepo(recipe, saveRecipe);

            return RedirectToAction(nameof(CooksSaved));

        }
        private void SaveToRepo(Recipe recipe, CookSavedRecipes savedRecipe)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInCook = _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefault();
            recipe.CookID = loggedInCook.CookId;

            _repo.CookSavedRecipes.Create(savedRecipe);
            _repo.Save();
        }

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
        public async Task<IActionResult> Review(int id)
        {
            //validate passed in RecipeId
            bool cookValidated = ValidateReviewSubmission(id);
            if (cookValidated == false)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var loggedInCook = await _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefaultAsync();
                Reviews review = new Reviews();
                review.RecipeID = id;
                review.CookId = loggedInCook.CookId;

                return View(review);

            }
            
        }

        private bool ValidateReviewSubmission(int id)
        {
            bool canSubmit = true;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInCook = _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefault();
            var reviewedRecipes = _repo.Reviews.FindByCondition(r => r.RecipeID == id);

            foreach(Reviews review in reviewedRecipes)
            {
                if (review.CookId == loggedInCook.CookId)
                {
                    canSubmit = false;
                    break;
                }
            }

            return canSubmit;
            
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
               
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Recipes/SeeReview/
        public IActionResult SeeReviews(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                List<Reviews> reviews = new List<Reviews>();

                var allReviews =  _repo.Reviews.FindAll().ToList();

                foreach(Reviews review in allReviews)
                {
                    if(review.RecipeID == id)
                    {
                        reviews.Add(review);
                    }
                }

                return View(reviews);
            }
        }

        public IActionResult GetEdits(int id)
        {
            //RecipeEdits recipeEdits = new RecipeEdits();
            List<string> listOfEdits = new List<string>();
            var recipeEdits = _repo.RecipeEdits.FindByCondition(r => r.RecipeID == id).ToList();
         
            return View(recipeEdits);
        }


        public IActionResult SubmitEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInCook = _repo.Cook.FindByCondition(e => e.IdentityUserId == userId).SingleOrDefault();
            var recipe = _repo.Recipe.FindByCondition(r => r.RecipeId == id).FirstOrDefault();

            RecipeEdits suggestedEdit= new RecipeEdits();
            suggestedEdit.RecipeID = recipe.RecipeId;
            suggestedEdit.CookId = loggedInCook.CookId;

            //return RedirectToAction(nameof(SubmitEdit));
            return View(suggestedEdit);
        }
        [HttpPost]
        public IActionResult AddEdit(RecipeEdits suggestedRecipeEdit)
        {
            _repo.RecipeEdits.Create(suggestedRecipeEdit);
            _repo.Save();

            return RedirectToAction("Index");
        }
    }
}
