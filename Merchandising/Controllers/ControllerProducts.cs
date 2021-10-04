using Merchandising.Entities;
using Merchandising.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		}

		[HttpGet]
		public IActionResult Get(
			[FromRoute] int? id,
			[FromQuery] string search,
			[FromQuery] int? minStock,
			[FromQuery] int? maxStock
		)
		{
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

		[HttpPost]
		public IActionResult Post(
				[FromBody] Product product
		) {
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Save(product)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		[HttpPut]
		public IActionResult Put(
				[FromBody] Product product
		)
		{
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Update(product)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		[HttpPatch]
		public IActionResult Patch(
				[FromBody] Product product
		)
		{
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Patch(product)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

		[HttpDelete]
		public IActionResult Delete([FromRoute] int id)
		{
			this._service.Delete(id);
			return new ContentResult
			{
				StatusCode = 204
			};
		}
	}
}
