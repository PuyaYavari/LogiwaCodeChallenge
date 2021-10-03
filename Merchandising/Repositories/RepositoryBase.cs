﻿using Merchandising.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
		}

        public virtual int Save()
        {
            return context.SaveChanges();
        }

        public virtual Entity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual Entity Insert(Entity entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public virtual void Delete(object id)
        {
            Entity entityToDelete = GetByID(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Entity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual Entity Update(Entity entityToUpdate)
        {
            Entity updated = dbSet.Attach(entityToUpdate).Entity;
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return updated;
        }
    }
}