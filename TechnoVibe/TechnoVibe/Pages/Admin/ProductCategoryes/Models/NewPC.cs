using TechnoVibe.Entity;

namespace TechnoVibe.Pages.Admin.ProductCategoryes.Models
{
    public class NewPC
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public int ProductId { get; set; }
        public short CategoryId { get; set; }
    }
}
