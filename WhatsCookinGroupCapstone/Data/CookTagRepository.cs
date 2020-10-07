using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsCookinGroupCapstone.Contracts;
using WhatsCookinGroupCapstone.Models;

namespace WhatsCookinGroupCapstone.Data
{
    public class CookTagRepository : RepositoryBase<CookTag>, ICookTagRepository
    {
        public CookTagRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
    }
   
}
