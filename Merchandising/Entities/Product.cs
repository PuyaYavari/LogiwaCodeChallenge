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

		[MaxLength(200)]
		[Column("TITLE")]
		[JsonProperty("Title")]
		public string Title { get; set; }

		[Column("DESCRIPTION")]
		[JsonProperty("Description")]
		public string Description { get; set; }

		[Required]
		[Column("STOCK")]
		[JsonProperty("Stock")]
		public int? Stock { get; set; }

		[Column("CATEGORY")]
		[JsonProperty("Category")]
		public virtual Category Category { get; set; }

		[Column("ACTIVE")]
		[JsonProperty("IsActive")]
		public bool IsActive { get; set; }
	}
}
