using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductPage.Commands
{
    public record AddNewProductCommand(Product newProduct) : IRequest<bool>;

    public class AddNewProductHandler : IRequestHandler<AddNewProductCommand, bool>
    {
        private readonly AppDBContext _context;
        public AddNewProductHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(request.newProduct);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
