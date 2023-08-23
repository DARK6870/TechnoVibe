using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoVibe.Entity
{
	[Table("Category")]
	public class Category
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key, Column(TypeName = ("tinyint"))]
		public byte CategoryId { get; set; }

		[Required]
		[StringLength(20), Column(TypeName = "nvarchar(20)")]
		public string CategoryName { get; set; }
		public ICollection<ProductCategory> ProductCategories { get; set; }
	}
}
