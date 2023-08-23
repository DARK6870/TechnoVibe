using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Products.Queryes
{
    public record GetProductByCategoryQuery(string Category) : IRequest<List<Product>>;

    public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategoryQuery, List<Product>>
    {
        private readonly AppDBContext _context;
        public GetProductByCategoryHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Products
                .Include(p => p.Brands)
                .Include(p => p.ProductCategories).ThenInclude(p => p.Categories)
                .Where(p => p.ProductCategories.Any(pc => pc.Categories.CategoryName == request.Category))
                .ToListAsync();

            return result;
        }

    }
}
