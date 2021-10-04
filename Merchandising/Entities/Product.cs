using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Merchandising.Entities
{
	[Table("Product")]
	public class Product
	{
		[Key]
		[Column("ID")]
		[JsonProperty("Id")]
		public int Id { get; set; }

		[Column("TITLE")]
		[MaxLength(200)]
		[JsonProperty("Title")]
		public string Title { get; set; }

		[Column("DESCRIPTION")]
		[JsonProperty("Description")]
		public string Description { get; set; }

		[Column("STOCK")]
		[JsonProperty("Stock")]
		public int? Stock { get; set; }

		[ForeignKey("Category")]
		[Column("CATEGORY")]
		[JsonProperty("Category")]
		public int? Category { get; set; }

		[Column("ACTIVE")]
		[JsonProperty("IsActive")]
		public bool IsActive { get; set; }
	}
}
