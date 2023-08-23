using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductPage.Commands
{
    public record UpdateProductCommand(Product updated) : IRequest<bool>;

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly AppDBContext _context;
        public UpdateProductHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _context.Products.Update(request.updated);
            int rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

    }
}
