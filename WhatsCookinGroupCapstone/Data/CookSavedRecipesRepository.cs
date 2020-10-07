using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsCookinGroupCapstone.Contracts;
using WhatsCookinGroupCapstone.Models;

namespace WhatsCookinGroupCapstone.Data
{
    public class CookSavedRecipesRepository : RepositoryBase<CookSavedRecipes>, ICookSavedRecipesRepository
    {
        public CookSavedRecipesRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }



    }
}
