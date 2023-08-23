using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Categoryes.Queryes
{
    public record GetCategoryByNameQuery(string name) : IRequest<List<Category>>;

    public class GetCategoryByNameHandler : IRequestHandler<GetCategoryByNameQuery, List<Category>>
    {
        private readonly AppDBContext _context;
        public GetCategoryByNameHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.Where(c => c.CategoryName.Contains(request.name)).ToListAsync();

            return result;
        }
    }
}
