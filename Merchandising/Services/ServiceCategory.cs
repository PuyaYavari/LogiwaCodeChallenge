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
		private readonly RepositoryCategory _repository;

		public ServiceCategory(RepositoryCategory repository)
		{
			this._repository = repository;
		}

		/// <summary>
		/// Queries all categories from databasse
		/// </summary>
		/// <returns>A list of all categories in the database</returns>
		public List<Category> Get()
		{
			return this._repository.Get().ToList();
		}

		/// <summary>
		/// Queries the category with the given ID from the database
		/// </summary>
		/// <param name="id">ID of the category to query</param>
		/// <returns>The category with the given ID</returns>
		public Category Get(int id)
		{
			return this._repository.GetByID(id);
		}

		/// <summary>
		/// Inserts given category into database
		/// </summary>
		/// <param name="category">Category to insert</param>
		/// <returns>The inserted category</returns>
		public Category Save(Category category)
		{
			Category inserted = this._repository.Insert(category);
			this._repository.Save();
			return inserted;
		}

		/// <summary>
		/// Updates a category in the database
		/// </summary>
		/// <param name="category">Category to update</param>
		/// <returns>Updated category</returns>
		public Category Update(Category category)
		{
			Category updated = this._repository.Update(category);
			this._repository.Save();
			return updated;
		}

		/// <summary>
		/// Updates given fields of the specified category
		/// </summary>
		/// <param name="category">Fields of the category to update (must include ID)</param>
		/// <returns>Updated Category</returns>
		public Category Patch(Category category)
		{
			Category categoryToUpdate = this.Get(category.Id);

			if (category.Title != null)
				categoryToUpdate.Title = category.Title;

			if (category.MinStock != null)
				categoryToUpdate.MinStock = category.MinStock;

			return Update(categoryToUpdate);
		}

		/// <summary>
		/// Deletes the category with the given ID
		/// </summary>
		/// <param name="id">ID of the category to delete</param>
		public void Delete(int id)
		{
			this._repository.Delete(id);
			this._repository.Save();
		}

	}
}
