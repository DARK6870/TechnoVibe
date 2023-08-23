using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Categoryes.Commands
{
    public record UpdateCategoryCommand(Category updated) : IRequest<bool>;

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly AppDBContext _context;
        public UpdateCategoryHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            _context.Categories.Update(request.updated);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
