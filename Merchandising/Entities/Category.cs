using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Merchandising.Entities
{
	[Table("Category")]
	public class Category
	{
		[Key]
		[Column("ID")]
		public int Id { get; set; }

		[Column("TITLE")]
		[MaxLength(200)]
		public string Title { get; set; }

		[Column("MIN_STOCK")]
		public int MinStock { get; set; }
	}
}
