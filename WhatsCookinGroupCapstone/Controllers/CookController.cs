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
                return View(selectedCook);
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
    }
}
