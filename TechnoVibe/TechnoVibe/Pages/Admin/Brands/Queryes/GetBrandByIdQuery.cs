using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Brands.Queryes
{
    public record GetBrandByIdQuery(short id) : IRequest<Brand>;

    public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, Brand>
    {
        private readonly AppDBContext _context;
        public GetBrandByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Brand> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            Brand result = await _context.Brands.FindAsync(request.id);
            return result;
        }
    }
}
