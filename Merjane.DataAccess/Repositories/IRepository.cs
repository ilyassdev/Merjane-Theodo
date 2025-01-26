using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Merjane.Entities;
using Microsoft.EntityFrameworkCore;

namespace Merjane.DataAccess.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetQuery();

        void UpdateEntityState(T entity, EntityState state);
        Task<List<T>> GetAllAsync();
    }
}
