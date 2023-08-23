using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoVibe.Entity
{
	[Table("ProductCategory")]
	public class ProductCategory
	{
		[Key]
		public int? Id { get; set; }


		[Required, ForeignKey(nameof(Categories))]
		public byte CategoryId { get; set; }
		public virtual Category Categories { get; set; }


		[Required ,ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }


	}
}
