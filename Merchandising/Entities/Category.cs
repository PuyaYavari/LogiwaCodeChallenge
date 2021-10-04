using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Merchandising.Entities
{
	[Table("Category")]
	public class Category
	{
		[Key]
		[Column("ID")]
		[JsonProperty("Id")]
		public int Id { get; set; }

		[Column("TITLE")]
		[MaxLength(200)]
		[JsonProperty("Title")]
		public string Title { get; set; }

		[Column("MIN_STOCK")]
		[JsonProperty("MinStock")]
		public int? MinStock { get; set; }
	}
}
