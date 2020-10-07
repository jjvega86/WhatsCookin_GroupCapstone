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


       
    }
}
