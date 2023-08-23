using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TechnoVibe.Entity;

namespace TechnoVibe.Pages.Products.Models
{
    public class AllProductModel
    {
        public List<Product> Products { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Category> Categories { get; set; }
        public string? categoryName { get; set; }
        public string actionName { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

    }
}
