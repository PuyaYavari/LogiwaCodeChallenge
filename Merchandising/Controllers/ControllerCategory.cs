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

		[HttpGet]
		public IActionResult Get(
			[FromRoute] int id
		) 
		{
			return new ContentResult
			{
				Content = JsonConvert.SerializeObject(this._service.Get(id)),
				ContentType = "application/json",
				StatusCode = 200
			};
		}

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

		[HttpPatch]
		public IActionResult Patch()
		{
			return new ContentResult
			{
				Content = "Patch",
				ContentType = "application/xml",
				StatusCode = 200
			};
		}

		[HttpDelete]
		public IActionResult Delete()
		{
			return new ContentResult
			{
				Content = "Delete",
				ContentType = "application/xml",
				StatusCode = 200
			};
		}
	}
}
