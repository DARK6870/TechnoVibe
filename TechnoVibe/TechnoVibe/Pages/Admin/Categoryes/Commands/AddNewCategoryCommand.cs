using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Categoryes.Commands
{
    public record AddNewCategoryCommand(Category newcategory) : IRequest<bool>;

    public class AddNewCategoryHandler : IRequestHandler<AddNewCategoryCommand, bool>
    {
        private readonly AppDBContext _context;
        public AddNewCategoryHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddNewCategoryCommand request, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(request.newcategory);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
