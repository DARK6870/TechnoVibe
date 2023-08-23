using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Brands.Queryes
{
    public record GetBrandByCategoryNameQuery(string name) : IRequest<List<Brand>>;

    public class GetBrandByCategoryNameHandler : IRequestHandler<GetBrandByCategoryNameQuery, List<Brand>>
    {
        private readonly AppDBContext _context;
        public GetBrandByCategoryNameHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> Handle(GetBrandByCategoryNameQuery request, CancellationToken cancellationToken)
        {

            var categ = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName.Contains(request.name));

            if (categ == null)
            {
                return new List<Brand>();
            }

            var productforcategory = await _context.ProductCategories
                .Where(pc => pc.CategoryId == categ.CategoryId)
                .ToListAsync();

            var pr = productforcategory.Select(pc => pc.ProductId);

            var product = await _context.Products
                .Where(p => pr.Contains(p.ProductId))
                .ToListAsync();

            var brand = product.Select(p => p.BrandId).Distinct();

            var brands = await _context.Brands
                .Where(b => brand.Contains(b.BrandId))
                .ToListAsync();

            return brands;
        }
    }
}
