using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TechnoVibe.Entity
{
	[Table("Brand")]
	public class Brand
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key, Column(TypeName = "smallint")]
		public short BrandId { get; set; }


		[Required]
		[StringLength(20), Column(TypeName = "nvarchar(20)")]
		public string BrandName { get; set; }


		public ICollection<Product> Products { get; set; }
	}
}
