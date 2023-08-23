using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Products.Queryes
{
    public record GetProductByIdQuery(int id) : IRequest<Product>;

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly AppDBContext _context;
        public GetProductByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Products.Include(b => b.Brands).FirstOrDefaultAsync(p => p.ProductId == request.id);
            return result;
        }
    }
}
