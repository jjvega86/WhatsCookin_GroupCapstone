using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WhatsCookinGroupCapstone.Models;

namespace WhatsCookinGroupCapstone.Contracts
{
    interface IRepositoryBase
    {
        public interface IRepositoryBase<T>
        {
            IQueryable<T> FindAll();
            IQueryable<T> FindByCondition(Expression<Func<T, bool>>
           expression);
            void Create(T entity);
            void Update(T entity);
            void Delete(T entity);
        }
        

        //public interface IRecipeRepository : IRepositoryBase<Recipe>
        //{

        //}
        //public interface IRecipeTagsRepository : IRepositoryBase<RecipeTags>
        //{

        //}
        //public interface IReviewsRepository : IRepositoryBase<Reviews>
        //{

        //}
        //public interface ITagsRepository : IRepositoryBase<Tags>
        //{

        //}
        //public interface IUserRepository : IRepositoryBase<User>
        //{

        //}

    }
}
