using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductPage.Commands
{
    public record DeleteProductByIdCommand(int id) : IRequest<bool>;

    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        private readonly AppDBContext _context;
        public DeleteProductByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _context.Products.FindAsync(request.id);
            if (product == null) return false;

            var productCategories = _context.ProductCategories.Where(pc => pc.ProductId == product.ProductId);
            _context.ProductCategories.RemoveRange(productCategories);

            _context.Products.Remove(product);

            int affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

    }
}
