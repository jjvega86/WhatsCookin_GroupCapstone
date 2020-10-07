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
        private ICookRepository _repo;

        public CookController(ICookRepository repo)
        {
            _repo = repo;

        }

        // GET: CookController
        public ActionResult Index()
        {
            var selectedCook = _repo.GetCook(1);

            return View(selectedCook);
        }

        // GET: CookController/Details/5
        public ActionResult Details(int id)
        {
            var selectedCook = _repo.GetCook(id);
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
                _repo.AddCook(cook);
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
            return View();
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
