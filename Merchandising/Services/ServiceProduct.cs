using Merchandising.Entities;
using Merchandising.Enums;
using Merchandising.Exceptions;
using Merchandising.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Merchandising.Services
{
	public class ServiceProduct
	{
		private readonly RepositoryProduct _repository;
		private readonly ServiceCategory _categoryService;

		public ServiceProduct(RepositoryProduct repository, ServiceCategory categoryService)
		{
			this._repository = repository;
			this._categoryService = categoryService;
		}

		public List<Product> Get(
			string search,
			int? minStock,
			int? maxStock
		)
		{
			//TODO: Create filter
			return this._repository.Get().ToList();
		}

		public Product Get(int id)
		{
			return this._repository.GetByID(id);
		}

		public Product Save(Product product)
		{
			if (string.IsNullOrEmpty(product.Title))
				throw new MerchandisingException(EnumErrors.NULL_TITLE, "Product must have a title.");

			if(product.Category != null)
			{
				Category category = _categoryService.Get((int)product.Category);
				if(category == null)
					throw new MerchandisingException(EnumErrors.UNKNOWN_CATEGORY, $"No category with ID {product.Category}");

				if (product.Stock != null && category.MinStock != null)
					product.IsActive = product.Stock >= category.MinStock;
				else
					product.IsActive = false;
			}

			Product inserted = this._repository.Insert(product);
			this._repository.Save();
			return inserted;
		}

		public Product Update(Product product)
		{
			if (string.IsNullOrEmpty(product.Title))
				throw new MerchandisingException(EnumErrors.NULL_TITLE, "Product must have a title.");
			product.IsActive = product.Category != null && product.Stock >= _categoryService.Get((int)product.Category).MinStock;
			Product updated = this._repository.Update(product);
			this._repository.Save();
			return updated;
		}

		public Product Patch(Product product)
		{
			Product productToUpdate = this.Get(product.Id);

			if (product.Title != null)
				productToUpdate.Title = product.Title;

			if (product.Description != null)
				productToUpdate.Description = product.Description;

			if (product.Stock != null)
				productToUpdate.Stock = product.Stock;

			if (product.Category != null)
				productToUpdate.Category = product.Category;

			productToUpdate.IsActive = productToUpdate.Category != null && productToUpdate.Stock >= _categoryService.Get((int)productToUpdate.Category).MinStock;

			return Update(productToUpdate);
		}

		public void Delete(int id)
		{
			this._repository.Delete(id);
			this._repository.Save();
		}
	}
}
