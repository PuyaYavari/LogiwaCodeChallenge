using Merchandising.Entities;
using Merchandising.Middlewares;
using Merchandising.Repositories;
using Merchandising.Repositories.Contracts;
using Merchandising.Services;
using Merchandising.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().ConfigureApiBehaviorOptions(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var modelState = actionContext.ModelState;
					return new BadRequestObjectResult("");
				};
			});
			services.AddApiVersioning();

			services.AddSwaggerGen();

			services.AddScoped<MerchandisingContext>();

			services.AddScoped<IRepository<Category>, RepositoryCategory>();
			services.AddScoped<ServiceCategory>();

			services.AddScoped<IRepository<Product>, RepositoryProduct>();
			services.AddScoped<ServiceProduct>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseMiddleware<ErrorHandlerMiddleware>();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Merchandising Api");
			});

		}
	}
}
