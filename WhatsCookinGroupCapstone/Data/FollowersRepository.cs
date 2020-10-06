using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsCookinGroupCapstone.Contracts;
using WhatsCookinGroupCapstone.Models;
using static WhatsCookinGroupCapstone.Contracts.IRepositoryBase;

namespace WhatsCookinGroupCapstone.Data
{
    public class FollowersRepository : RepositoryBase<Followers>, IFollowersRepository
    {
        public FollowersRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

    }
}
