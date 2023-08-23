using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoVibe.Entity
{
	[Table("Status")]
	public class Status
	{
		[Key, Column(TypeName = "tinyint")]
		public byte StatusId { get; set; }

		[Required, StringLength(20), Column(TypeName = "nvarchar(20)")]
		public string StatusName { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
