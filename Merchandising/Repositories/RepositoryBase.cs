using Merchandising.Utils;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Merchandising.Repositories
{
	public class RepositoryBase<Entity> where Entity : class
	{
		internal MerchandisingContext context;
		internal DbSet<Entity> dbSet;

		public RepositoryBase(MerchandisingContext context)
		{
			this.context = context;
			this.dbSet = context.Set<Entity>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        public virtual int Save()
        {
            return context.SaveChanges();
        }

        public virtual IEnumerable<Entity> Get(
            Expression<Func<Entity, bool>> filter = null,
            Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null,
            string includeProperties = ""
        )
        {
            IQueryable<Entity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
                Log.Debug("Filter added to query.");
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
                Log.Debug($"{includeProperty} to query.");
            }
            if (orderBy != null)
            {
                Log.Debug("Ordered result is being returned.");
                return orderBy(query).ToList();
            }
            else
            {
                Log.Debug("Result is being returned.");
                return query.ToList();
            }
        }

        public virtual Entity GetByID(
            object id
        )
        {
            Log.Debug($"Entity with id {id} is being returned.");
            return dbSet.Find(id);
        }

        public virtual Entity Insert(Entity entity)
        {
            Log.Debug($"Entity is being inserted.");
            return dbSet.Add(entity).Entity;
        }

        public virtual void Delete(object id)
        {
            Log.Debug($"Entity with id {id} is being deleted.");
            Entity entityToDelete = GetByID(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Entity entityToDelete)
        {
            Log.Debug($"Entity with is being deleted.");
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual Entity Update(Entity entityToUpdate)
        {
            Log.Debug($"Entity is being updated.");
            Entity updated = dbSet.Attach(entityToUpdate).Entity;
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return updated;
        }
    }
}
