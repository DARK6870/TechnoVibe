using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace TechnoVibe.Entity
{
    public class AppUser : IdentityUser
    {
        [NotMapped]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
