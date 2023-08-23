using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TechnoVibe.Entity;

namespace TechnoVibe.Pages.Admin.ProductPage.Models
{
    public class NewProduct
    {
        public List<Brand> Brands { get; set; }
        public int ProductId { get; set; }
        public short BrandId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte? Sale { get; set; }
        public short Quanity { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
    }
}
