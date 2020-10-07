using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsCookinGroupCapstone.Contracts;
using WhatsCookinGroupCapstone.Models;

namespace WhatsCookinGroupCapstone.Data
{
    public class PreferencesRepository : RepositoryBase<Preferences>, IPreferencesRepository
    {
        public PreferencesRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

    }
}
