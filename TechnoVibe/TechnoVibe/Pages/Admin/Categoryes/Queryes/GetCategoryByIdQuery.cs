using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Categoryes.Queryes
{
    public record GetCategoryByIdQuery(byte id) : IRequest<Category>;

    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly AppDBContext _context;
        public GetCategoryByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            Category? result = await _context.Categories.FindAsync(request.id);

            return result;
        }
    }
}
