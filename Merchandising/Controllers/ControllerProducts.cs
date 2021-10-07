using Merchandising.Entities;
using Merchandising.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Merchandising.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/Products/{id?}")]
	[ApiVersion("1.0")]
	public class ControllerProducts : Controller
	{
		private ServiceProduct _service;

		public ControllerProducts(ServiceProduct service)
		{
			this._service = service;

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console()
				.CreateLogger();
		}

		/// <summary>
		/// Queries and return products from database
		/// </summary>
		/// <param name="id">Id of the product to query</param>
		/// <param name="search">Search for products with this string in title, description or category name</param>
		/// <param name="minStock">Search for products with stock larger than this parameter</param>
		/// <param name="maxStock">Search for products with stock less than this parameter</param>
		/// <returns>A list of products</returns>
		[HttpGet]
		public IActionResult Get(
			[FromRoute] int? id,
			[FromQuery] string search,
			[FromQuery] int? minStock,
			[FromQuery] int? maxStock
		)
		{
			Log.Information($"Request received. GET Products. id:{id}, search:{search}, minStock:{minStock}, maxStock:{maxStock}");
			if (id == null)
			{
				return new ContentResult
				{
					Content = JsonConvert.SerializeObject(this._service.Get(search, minStock, maxStock)),
					ContentType = "application/json",
					StatusCode = 200
				};
			} else
			{
				return new ContentResult
				{
					Content = JsonConvert.SerializeObject(this._service.Get((int)id)),
					ContentType = "application/json",
					StatusCode = 200
				};
			}
		}

		/// <summary>
		/// Inserts given product into database
		/// </summary>
		/// <param name="product">Product to insert</param>
		/// <returns>Inserted product</returns>
		[HttpPost]
		public IActionResult Post(
				[FromBody] Product product
		)
		{
			Log.Information($"Request received. POST Product.");
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Save(product)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		/// <summary>
		/// Updates product with the given Id
		/// </summary>
		/// <param name="id">Id of the product to update</param>
		/// <param name="product">Updated product</param>
		/// <returns>Updated product</returns>
		[HttpPut]
		public IActionResult Put(
			[FromRoute] int id,
			[FromBody] Product product
		)
		{
			Log.Information($"Request received. PUT Product. id:{id}");
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Update(id, product)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		/// <summary>
		/// Updates given fields of the product with the given Id
		/// </summary>
		/// <param name="id">Id of the product to update</param>
		/// <param name="product">Updated fields</param>
		/// <returns>Updated product</returns>
		[HttpPatch]
		public IActionResult Patch(
			[FromRoute] int id,
			[FromBody] Product product
		)
		{
			Log.Information($"Request received. PATCH Product. id:{id}");
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Patch(id, product)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		/// <summary>
		/// Deletes product with the given Id
		/// </summary>
		/// <param name="id">Id of the product to delete</param>
		/// <returns>No Content</returns>
		[HttpDelete]
		public IActionResult Delete([FromRoute] int id)
		{
			Log.Information($"Request received. DELETE Product. id:{id}");
			this._service.Delete(id);
			return new ContentResult
			{
				StatusCode = 204
			};
		}
	}
}
