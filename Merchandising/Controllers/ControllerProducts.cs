using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/Products")]
	[ApiVersion("1.0")]
	public class ControllerProducts : Controller
	{
		[HttpGet]
		public IActionResult Get()
		{
			return new ContentResult
			{
				Content = "Get",
				ContentType = "application/xml",
				StatusCode = 200
			};
		}

		[HttpPost]
		public IActionResult Post()
		{
			return new ContentResult
			{
				Content = "Post",
				ContentType = "application/xml",
				StatusCode = 200
			};
		}

		[HttpPut]
		public IActionResult Put()
		{
			return new ContentResult
			{
				Content = "Put",
				ContentType = "application/xml",
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
