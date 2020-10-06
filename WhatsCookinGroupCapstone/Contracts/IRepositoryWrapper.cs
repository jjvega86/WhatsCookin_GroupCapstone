using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WhatsCookinGroupCapstone.Contracts.IRepositoryBase;

namespace WhatsCookinGroupCapstone.Contracts
{
    public interface IRepositoryWrapper
    {
        ICookRepository Cook { get; }
        IFollowersRepository Followers { get; }
        IRecipeRepository Recipe { get; }
        IRecipeTagsRepository RecipeTags { get; }
        IReviewsRepository Reviews { get; }
        ITagsRepository Tags { get; }

        void Save();
    }
}
