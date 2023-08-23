using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoVibe.Entity
{
    public class Support
    {
        [Key]
        public int SupportId { get; set; }

        [Required, StringLength(40), Column(TypeName = "nvarchar(40)")]
        public string Email { get; set; }

        [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string Problem { get; set; }

        [Required, StringLength(400), Column(TypeName = "nvarchar(400)")]
        public string ProblemDescription { get; set; }
    }
}
