using Merchandising.Entities;
using Merchandising.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising.Services
{
	public class ServiceCategory
	{
		private RepositoryCategory _repository;

		public ServiceCategory(RepositoryCategory repository)
		{
			this._repository = repository;
		}

		public Category Get(int id)
		{
			return this._repository.GetByID(id);
		}

		public Category Save(Category category)
		{
			Category inserted = this._repository.Insert(category);
			this._repository.Save();
			return inserted;
		}

		public Category Update(Category category)
		{
			Category updated = this._repository.Update(category);
			this._repository.Save();
			return updated;
		}

	}
}
