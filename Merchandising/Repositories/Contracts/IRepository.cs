using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Merchandising.Repositories.Contracts
{
	public interface IRepository<Entity> where Entity : class
	{
		int Save();

		IEnumerable<Entity> Get(
			Expression<Func<Entity, bool>> filter = null,
			Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy = null,
			string includeProperties = ""
		);

		Entity GetByID(object id);

		Entity Insert(Entity entity);

		void Delete(object id);

		void Delete(Entity entityToDelete);

		Entity Update(Entity entityToUpdate);
	}
}
