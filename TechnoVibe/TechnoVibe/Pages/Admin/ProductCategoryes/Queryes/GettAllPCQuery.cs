using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductCategoryes.Queryes
{
    public record GetAllPCQuery() : IRequest<List<ProductCategory>>;

    public class GetAllPCHandler : IRequestHandler<GetAllPCQuery, List<ProductCategory>>
    {
        private readonly AppDBContext _context;
        public GetAllPCHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCategory>> Handle(GetAllPCQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.ProductCategories.ToListAsync();
            return result;
        }
    }
}
