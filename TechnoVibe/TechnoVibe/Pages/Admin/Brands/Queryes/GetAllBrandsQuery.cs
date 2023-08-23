using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Brands.Queryes
{
    public record GetAllBrandsQuery() : IRequest<List<Brand>>;

    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, List<Brand>>
    {
        private readonly AppDBContext _context;
        public GetAllBrandsHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Brands.ToListAsync();
            return result;
        }
    }
}
