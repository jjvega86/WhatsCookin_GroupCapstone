using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsCookinGroupCapstone.Contracts;
using WhatsCookinGroupCapstone.Models;
using static WhatsCookinGroupCapstone.Contracts.IRepositoryBase;

namespace WhatsCookinGroupCapstone.Data
{
    public class CookRepository : RepositoryBase<Cook>, ICookRepository
    {
        public CookRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }


        public Cook GetCook (int cookId)
        {
            return ApplicationDbContext.Cook.Find(cookId);
        }

        public void AddCook (Cook cook)
        {
            ApplicationDbContext.Cook.Add(cook);
        }

        public void Save()
        {
            ApplicationDbContext.SaveChanges();
        }
    }
}
