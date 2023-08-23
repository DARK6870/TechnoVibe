using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Categoryes.Queryes
{
    public record GetAllCategoryesQuery() : IRequest<List<Category>>;

    public class GetAllCategoryesHandler : IRequestHandler<GetAllCategoryesQuery, List<Category>>
    {
        private readonly AppDBContext _context;
        public GetAllCategoryesHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Handle(GetAllCategoryesQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.ToListAsync();
            return result;
        }
    }
}
