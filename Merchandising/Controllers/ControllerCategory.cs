using Merchandising.Entities;
using Merchandising.Services;
using Merchandising.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/Categories/{id?}")]
	[ApiVersion("1.0")]
	public class ControllerCategory : Controller
	{
		private ServiceCategory _service;

		public ControllerCategory(ServiceCategory service)
		{
			this._service = service;
		}

		/// <summary>
		/// Queries and returns categories from database
		/// </summary>
		/// <param name="id">Category ID</param>
		/// <returns>A list of all categories if ID is null else  returns the category with the given ID</returns>
		[HttpGet]
		public IActionResult Get(
			[FromRoute] int? id
		) 
		{
			if(id == null)
			{
				return new ContentResult
				{
					Content = JsonConvert.SerializeObject(this._service.Get()),
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
		/// Inserts the given category to database
		/// </summary>
		/// <param name="category">Category to insert</param>
		/// <returns>Inserted Category</returns>
		[HttpPost]
		public IActionResult Post(
			[FromBody] Category category
		)
		{
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Save(category)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		/// <summary>
		/// Updates the entire category
		/// </summary>
		/// <param name="category">Category to update</param>
		/// <returns>Updated category</returns>
		[HttpPut]
		public IActionResult Put(
			[FromBody] Category category
		)
		{
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Update(category)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		/// <summary>
		/// Updates given fields of the category
		/// </summary>
		/// <param name="category">Fields of the category to update (must include ID)</param>
		/// <returns>Updated Category</returns>
		[HttpPatch]
		public IActionResult Patch(
			[FromBody] Category category
		)
		{
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Patch(category)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		/// <summary>
		/// Deletes the category with the given ID
		/// </summary>
		/// <param name="id">ID of the category to delete</param>
		/// <returns>No content</returns>
		[HttpDelete]
		public IActionResult Delete(
			[FromRoute] int id
		)
		{
			this._service.Delete(id);
			return new ContentResult
			{
				StatusCode = 204
			};
		}
	}
}
