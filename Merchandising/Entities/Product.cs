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
		public int Id { get; set; }

		[Column("TITLE")]
		[MaxLength(200)]
		public string Title { get; set; }

		[Column("STOCK")]
		public int Stock { get; set; }

		[ForeignKey("ID")]
		[Column("CATEGORY")]
		public Category Category { get; set; }

		[Column("CREATION_TIME")]
		public DateTime CreationTime { get; set; }

		[Column("ACTIVE")]
		public bool IsActive { get; set; }
	}
}
