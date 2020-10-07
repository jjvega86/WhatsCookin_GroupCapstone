using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var selectedCook = _repo.Cook.FindByCondition(r => r.CookId == 1).SingleOrDefault();

            return View(selectedCook);
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
            return View(cook);
        }   

        // POST: CookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cook cook)
        {
            try
            {
                _repo.Cook.Create(cook);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}
