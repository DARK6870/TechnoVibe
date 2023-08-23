using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoVibe.Entity
{
	[Table("Product")]
	public class Product
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int ProductId { get; set; }


		[Required, ForeignKey(nameof(Brands))]
		public short BrandId { get; set; }
		public virtual Brand Brands { get; set; }


		[Required]
		[StringLength(50), Column(TypeName = "nvarchar(50)")]
		public string ProductName { get; set; }


		[Required]
		[StringLength(300), Column(TypeName = "nvarchar(300)")]
		public string Description { get; set; }


		[Required]
		[Column(TypeName = "Decimal(10,2)")]
		public decimal Price { get; set; }


		[Column(TypeName = "tinyint")]
		public byte? Sale { get; set; }


		[Required]
		[Column(TypeName = "smallint")]
		public short Quanity { get; set; }

		[Required]
		[Column(TypeName = "varchar(200)")]
		public string Image1 { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Image2 { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Image3 { get; set; }

        public ICollection<Order> Orders { get; set; }
		public ICollection<ProductCategory> ProductCategories { get; set; }

	}
}
