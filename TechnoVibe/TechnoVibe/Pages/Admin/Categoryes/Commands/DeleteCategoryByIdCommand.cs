using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Categoryes.Commands
{
    public record DeleteCategoryByIdCommand(byte id) : IRequest<bool>;

    public class DeleteCategoryByIdHandler : IRequestHandler<DeleteCategoryByIdCommand, bool>
    {
        private readonly AppDBContext _context;
        public DeleteCategoryByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            Category? cat = await _context.Categories.FindAsync(request.id);
            if (cat == null) return false;

            var relatedProductCategories = _context.ProductCategories.Where(pc => pc.CategoryId == cat.CategoryId);
            _context.ProductCategories.RemoveRange(relatedProductCategories);

            _context.Categories.Remove(cat);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
