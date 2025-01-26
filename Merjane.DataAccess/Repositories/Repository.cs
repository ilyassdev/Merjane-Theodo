using Merjane.DataAccess.Context;
using Merjane.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Merjane.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MerjaneDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MerjaneDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #region Get Queryable
        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }
        #endregion

        #region Update the entity state
        public void UpdateEntityState(T entity, EntityState state)
        {
            _context.Entry(entity).State = state;
        }
        #endregion

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();

        }
    }
}
