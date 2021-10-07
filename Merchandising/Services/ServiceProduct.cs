using Merchandising.Entities;
using Merchandising.Enums;
using Merchandising.Exceptions;
using Merchandising.Repositories;
using Merchandising.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Merchandising.Services
{
	public class ServiceProduct
	{
		private readonly IRepository<Product> _repository;
		private readonly ServiceCategory _categoryService;

		public ServiceProduct(IRepository<Product> repository, ServiceCategory categoryService)
		{
			this._repository = repository;
			this._categoryService = categoryService;
		}

		/// <summary>
		/// Quersies products from database based on given search parameter and, min and max  stocks
		/// </summary>
		/// <param name="search">The keyword to search in products title, description or products category title</param>
		/// <param name="minStock">Query products with stock larger than this number</param>
		/// <param name="maxStock">Query products with stock less than this number</param>
		/// <returns>A list of products</returns>
		public List<Product> Get(
			string search,
			int? minStock,
			int? maxStock
		)
		{
			Expression<Func<Product, bool>> filter = p =>
				(string.IsNullOrEmpty(search) ? true : (p.Title.Contains(search) || p.Description.Contains(search) || (p.Category == null ? true : p.Category.Title.Contains(search)))) &&
				(minStock == null ? true : p.Stock > minStock) && (maxStock == null ? true : p.Stock < maxStock);
			return this._repository.Get(filter, includeProperties: "Category").ToList();
		}

		/// <summary>
		/// Queries product with the given Id
		/// </summary>
		/// <param name="id">Id of the product</param>
		/// <returns>Product with the given Id</returns>
		public Product Get(int id)
		{
			Expression<Func<Product, bool>> filter = p => p.Id == id;
			return this._repository.Get(filter, includeProperties: "Category").FirstOrDefault();
		}

		/// <summary>
		/// Inserts given product into database
		/// </summary>
		/// <param name="product">The product to insert into database</param>
		/// <returns>Inserted product</returns>
		public Product Save(Product product)
		{
			if (string.IsNullOrEmpty(product.Title))
				throw new MerchandisingException(EnumErrors.NULL_TITLE, "Product must have a title.");

			if(product.Category != null)
			{
				Category category = product.Category;
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

		/// <summary>
		/// Updates a product entirely
		/// </summary>
		/// <param name="product">Product to update</param>
		/// <param name="id">ID of the product to update</param>
		/// <returns>Updated product</returns>
		public Product Update(int id, Product product)
		{
			if (string.IsNullOrEmpty(product.Title))
				throw new MerchandisingException(EnumErrors.NULL_TITLE, "Product must have a title.");
			product.Id = id;
			product.IsActive = product.Category != null && product.Stock >= product.Category.MinStock;
			Product updated = this._repository.Update(product);
			this._repository.Save();
			return updated;
		}

		/// <summary>
		/// Updates given fields of the product
		/// </summary>
		/// <param name="product">Fields of the product to update</param>
		/// <param name="id">ID of the product to update</param>
		/// <returns>New form of the product</returns>
		public Product Patch(int id, Product product)
		{
			Product productToUpdate = this.Get(id);

			if (product.Title != null)
				productToUpdate.Title = product.Title;

			if (product.Description != null)
				productToUpdate.Description = product.Description;

			if (product.Stock != null)
				productToUpdate.Stock = product.Stock;

			if (product.Category != null)
			{
				if (productToUpdate.Category != null)
				{
					if (product.Category.Title != null)
						productToUpdate.Category.Title = product.Category.Title;

					if (product.Category.MinStock != null)
						productToUpdate.Category.MinStock = product.Category.MinStock;
				} else
				{
					productToUpdate.Category = product.Category;
				}

			}

			productToUpdate.IsActive = productToUpdate.Category != null && productToUpdate.Stock >= productToUpdate.Category.MinStock;

			return Update(id, productToUpdate);
		}

		/// <summary>
		/// Deletes the product with the given Id from the database
		/// </summary>
		/// <param name="id">ID of the product to delete</param>
		public void Delete(int id)
		{
			this._repository.Delete(id);
			this._repository.Save();
		}
	}
}
