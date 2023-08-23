using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductCategoryes.Commands
{
    public record AddNewPCCommand(ProductCategory newcategory) : IRequest<bool>;

    public class AddNewPCHandler : IRequestHandler<AddNewPCCommand, bool>
    {
        private readonly AppDBContext _context;
        public AddNewPCHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddNewPCCommand request, CancellationToken cancellationToken)
        {
            await _context.ProductCategories.AddAsync(request.newcategory);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
