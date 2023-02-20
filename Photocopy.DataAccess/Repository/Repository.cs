using Microsoft.EntityFrameworkCore;
using Photocopy.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
           await Context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);

        }
		public async Task <TEntity> Update(TEntity entity)
        {
            Context.Update(entity);
			return entity;
        }


		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public  IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return  Context.Set<TEntity>().Where(predicate).AsNoTracking();
        }


        public TEntity GetByIdAsync(Expression<Func<TEntity,bool>> predicate)

		{
            return  Context.Set<TEntity>().Where(predicate).SingleOrDefault();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);

        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }


	}
}
