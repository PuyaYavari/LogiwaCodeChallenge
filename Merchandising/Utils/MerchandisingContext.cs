using Merchandising.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising.Utils
{
	public class MerchandisingContext : DbContext
	{
		private readonly string DbPath;

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }

		public MerchandisingContext()
		{
			var path = AppDomain.CurrentDomain.BaseDirectory;
			DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}Merchandising.db";
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");
	}
}
