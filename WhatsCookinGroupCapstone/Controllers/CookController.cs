using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WhatsCookinGroupCapstone.Contracts;
using WhatsCookinGroupCapstone.Models;
using WhatsCookinGroupCapstone.Models.View_Model;

namespace WhatsCookinGroupCapstone.Controllers
{
    public class CookController : Controller
    {
        private IRepositoryWrapper _repo;


        public CookController(IRepositoryWrapper repo)
        {
            _repo = repo;

        }

        // GET: CookController
        // Default view: will show a grid of multiple recipes and cooks you are following
        public ActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var selectedCook = _repo.Cook.FindByCondition(r => r.IdentityUserId == userId).SingleOrDefault();

            if (selectedCook == null)
            {
                return RedirectToAction("Create");

            }
            else
            {
                //This only works if recipes are seeded in the database.  
                //Maybe come back later and make new view for if in case no recipes exist.
                UserRandomRecipes userRandomRecipes1 = new UserRandomRecipes();
                List<Recipe> recipeList = FindMatchingRecipes(FindRecipeTagsMatchingCookTags(FindCookTags(selectedCook)));
                List<Recipe> finalRecipeList = RandomizeRecipes(recipeList);
                ConvertListToModelViewType(finalRecipeList);
                var theActualFinalList = ConvertListToModelViewType(finalRecipeList);
                var feelinLuckyRecipe = (FindRecipeForFeelinLuckyButton(1));
                var theViewObject = AddFeelinLuckyToViewObject(theActualFinalList, feelinLuckyRecipe);
                return View(theViewObject);

            }

        }

        // GET: CookController/Details/5
        public ActionResult Details(int id)
        {
            var selectedCook = _repo.Cook.FindByCondition(r => r.CookId == 1).SingleOrDefault();
            return View(selectedCook);
        }

        // GET: CookController/Create
        public ActionResult Create()
        {
            Cook cook = new Cook();
            {
                cook.AllTags = GetTags();
            }


            return View(cook);
        }

        // POST: CookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cook cook)
        {

            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                cook.IdentityUserId = userId;
                _repo.Cook.Create(cook);
                _repo.Save();

                var selectedCook = _repo.Cook.FindByCondition(c => c.IdentityUserId == userId).SingleOrDefault();
                var selectedCookId = selectedCook.CookId;

                foreach (string tag in cook.SelectedTags)
                {
                    var selectedTag = _repo.Tags.FindByCondition(r => r.Name == tag).SingleOrDefault();

                    CookTag cookTag = new CookTag();
                    cookTag.CookId = selectedCookId;
                    cookTag.TagsId = selectedTag.TagsId;
                    _repo.CookTag.Create(cookTag);
                    _repo.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cook);
        }


        // GET: CookController/Edit/5
        public ActionResult Edit(int id)
        {
            var selectedCook = _repo.Cook.FindByCondition(r => r.CookId == id).SingleOrDefault();
            return View(selectedCook);
        }

        // POST: CookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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


        //I want to find all of the tags for my cook **
        // then, I want to find all recpies tagged with the same tags related to my cook**
        // i.e., cook only wants vegan recipes ("TagId = 1"), we then query the RecipeTags table and find all recipes with a
        // TagId == 1. I want to have a list of RecipeIds from this query**
        // then, I want to query my recipes table for all of the recipes that match list of RecipeIds and add them to a list**
        // Then, I want to pass six of those recipes to a view and display them as a grid that the cook can see
        // Ideally, I want to randomly select the six recipes
        private List<int> FindCookTags(Cook cook)
        {
            var selectedCook = _repo.Cook.FindByCondition(c => c.CookId == cook.CookId).SingleOrDefault();
            var cookTags = _repo.CookTag.FindByCondition(c => c.CookId == selectedCook.CookId);
            List<int> recipeTags = new List<int>();
            foreach (CookTag cookTag in cookTags)
            {
                recipeTags.Add(cookTag.TagsId);
            }
            return recipeTags;
        }
        private List<int> FindRecipeTagsMatchingCookTags(List<int> recipeTags)
        {
            List<int> recipeIds = new List<int>();
            foreach (int tagId in recipeTags)
            {

                var selectedRecipe = _repo.RecipeTags.FindByCondition(c => c.TagsId == tagId).FirstOrDefault();

                if(selectedRecipe == null)
                {
                    var firstRecipe = _repo.Recipe.FindByCondition(r => r.RecipeId == 1).SingleOrDefault();

                    recipeIds.Add(firstRecipe.RecipeId);
                  
                }
                else
                {
                    recipeIds.Add(selectedRecipe.RecipeId);

                }
            }
            return recipeIds;
        }
        private List<Recipe> FindMatchingRecipes(List<int> recipeIds)
        {
            List<Recipe> recipeList = new List<Recipe>();
            foreach (int recipeId in recipeIds)
            {
                var selectedRecipe = _repo.Recipe.FindByCondition(c => c.RecipeId == recipeId).SingleOrDefault();
                recipeList.Add(selectedRecipe);
            }
            return recipeList;
        }
        private HashSet<int> GetSixRandomNumbers(int recipeCount)
        {
            //Hashset stops two numbers repeating more than once from random
            HashSet<int> sixRandomNumbers = new HashSet<int>();
            //Excludes 0 from being available in hashset

            Random random = new Random();
            while (sixRandomNumbers.Count < 6 ) //could be < recipecount to always give the exact amount of random numbers back thats needed
            {
                sixRandomNumbers.Add(random.Next(0, recipeCount));
            }
           
            return sixRandomNumbers;
        }

        private HashSet<int> GetVariousAmountsOfRandomNumbers(int recipeCount)
        {
            HashSet<int> randomNumbers = new HashSet<int>();
            Random rand = new Random();


            while (randomNumbers.Count < recipeCount)

            {
                randomNumbers.Add(rand.Next(0, recipeCount));
            }

            return randomNumbers;

        }
        private List<Recipe> RandomizeRecipes(List<Recipe> recipeList)
        {
            // recipeList generated from FindMatchingRecipes
            // set int recipeCount parameter for GetSixRandomNumbers = to recipeList.Count-1
            //int recipeCount = 1;

            // if there are less than six recipes in the list, add the first several recipes from the database until there are six in the list



            var listOfAllRecipes = _repo.Recipe.FindAll().ToList();

            while (recipeList.Count < 6)
            {
                //this takes two lists and removes values that are present only in both lists.
                var result = listOfAllRecipes.Concat(recipeList)
                .GroupBy(x => x.RecipeId)
                    .Where(x => x.Count() == 1)
                    .Select(x => x.FirstOrDefault())
                    .ToList();
<<<<<<< HEAD
=======
                //var randomnumbernow = GetVariousAmountsOfRandomNumbers(result.Count);
                //foreach (int number in randomnumbernow)
                //{
                //    recipeList.Add(result[number]);
                //    recipeCount++;
                //}


>>>>>>> c79cb08cd5917d34f4cf3e16b6b542441002b0f3
                var randomnumbernow = GetVariousAmountsOfRandomNumbers(result.Count);
                foreach (int number in randomnumbernow)
                {
                    recipeList.Add(result[number]);
<<<<<<< HEAD
=======
                    //recipeCount++;
>>>>>>> c79cb08cd5917d34f4cf3e16b6b542441002b0f3
                }
            }

            var recipeCount = recipeList.Count();
            
            var randomNumbers = GetSixRandomNumbers(recipeCount);
            List<Recipe> finalRecipeList = new List<Recipe>();

            foreach (int randomNumber in randomNumbers)
            {
                var recipe = recipeList[randomNumber];
                finalRecipeList.Add(recipe);
            }

            return finalRecipeList;
        }

        private UserRandomRecipes ConvertListToModelViewType(List<Recipe> finalRecipeList)
        {

            UserRandomRecipes userRandomRecipes = new UserRandomRecipes()
            {
                Recipes = new List<Recipe>()
            };
            List<UserRandomRecipes> rec = new List<UserRandomRecipes>();

            foreach (Recipe recipe in finalRecipeList)
            {

                userRandomRecipes.Recipes.Add(recipe);
                rec.Add(userRandomRecipes);

            }
            return userRandomRecipes;
        }

        private HashSet<int> GetOneRandomNumber(int recipeCount)
        {

            HashSet<int> getOneRandom = new HashSet<int>();
            Random random = new Random();

            var allRecipeCount = _repo.Recipe.FindAll();
            int numberofRecipes = allRecipeCount.Count();

            getOneRandom.Add(random.Next(1, numberofRecipes + 1));

            return getOneRandom;
        }

        public ActionResult Follow(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInCook = _repo.Cook.FindByCondition(r => r.IdentityUserId == userId).SingleOrDefault();
            var selectedRecipe = _repo.Recipe.FindByCondition(r => r.RecipeId == id).SingleOrDefault();
            var cookToFollow = _repo.Cook.FindByCondition(r => r.CookId == selectedRecipe.CookID).SingleOrDefault();

            Followers follower = new Followers();

            // The follower CookId is always the cook who cooked the recipe the logged in cook is viewing
            // The UserName is always the cook who cooked the recipe
            // The FollowerId is always the loggedInCook doing the following

            follower.CookID = cookToFollow.CookId;
            follower.UserName = cookToFollow.UserName;
            follower.FollowerId = loggedInCook.CookId;

            _repo.Followers.Create(follower);
            _repo.Save();

            return RedirectToAction(nameof(Followers));

        }

        public ActionResult Followers()
        {
            // I want to show a list of all of the cooks the logged in cook is following
            // Find the logged in cook
            // Query the Followers table for the all entries with a FollowerId and store those objects to a list
            // Query the Cook table for matching cooks
            // Return a List<Cook> of followed cooks to a Followers View

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInCook = _repo.Cook.FindByCondition(r => r.IdentityUserId == userId).SingleOrDefault();

            var listOfFollowers = _repo.Followers.FindByCondition(r => r.FollowerId == loggedInCook.CookId).ToList();
            List<Cook> followedCooks = new List<Cook>();

            foreach (Followers follower in listOfFollowers)
            {
                var followedCook = _repo.Cook.FindByCondition(c => c.CookId == follower.CookID).SingleOrDefault();
                followedCooks.Add(followedCook);
            }

            return View(followedCooks);


        }

        private Recipe FindRecipeForFeelinLuckyButton(int recipeCount)
        {
            HashSet<int> oneRandomNumber = GetOneRandomNumber(recipeCount);

            Recipe feelinLuckyObject = new Recipe();

            foreach (int randomNumber in oneRandomNumber)
            {
                feelinLuckyObject = _repo.Recipe.FindByCondition(r => r.RecipeId == randomNumber).SingleOrDefault();
                //feelinLuckyObject.Add(recipe);
            }

            return feelinLuckyObject;
        }

        private UserRandomRecipes AddFeelinLuckyToViewObject(UserRandomRecipes finalList, Recipe feelinLucky)
        {


            var helperObject = _repo.Recipe.FindByCondition(f => f.RecipeId == feelinLucky.RecipeId).SingleOrDefault();

            finalList.FeelinLucky = helperObject;
            return finalList;
        }



        public async Task<IActionResult> FollowedCookbook(int id)

        {
            // I want to return a list of recipes that the passed in CookId has created from the Recipe database

            var listOfRecipes = _repo.Recipe.FindByCondition(r => r.CookID == id).ToList();

            return View(listOfRecipes);
        }

    }
}




