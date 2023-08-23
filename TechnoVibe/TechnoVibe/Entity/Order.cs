using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoVibe.Entity
{
	[Table("Order")]
	public class Order
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key, Column(TypeName = "int")]
		public int OrderId { get; set; }


		[Required, ForeignKey(nameof(Products))]
		public int ProductId { get; set; }
		public virtual Product Products { get; set; }


		[Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
		public string FullName { get; set; }


		[Required]
		[StringLength(15), Column(TypeName = "varchar(15)")]
		public string phoneNumber { get; set; }


		[Required]
		[StringLength(50), Column(TypeName = "nvarchar(50)")]
		public string Address { get; set; }


		[Required, ForeignKey(nameof(Statuss))]
		public byte StatusId { get; set; }
		public Status Statuss { get; set; }


		[Required]
		[Column(TypeName = "Decimal(10,2)")]
		public decimal TotalPrice { get; set; }

		[ForeignKey(nameof(AppUsers))]
		public string? AppUserId { get; set; }
		public virtual AppUser? AppUsers { get; set; }


	}
}
