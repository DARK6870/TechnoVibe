using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Brands.Commands
{
    public record DeleteBrandByIdCommand(short id) : IRequest<bool>;

    public class DeleteBrandByIdHandler : IRequestHandler<DeleteBrandByIdCommand, bool>
    {
        private readonly AppDBContext _context;
        public DeleteBrandByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBrandByIdCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _context.Brands.FindAsync(request.id);
            if (brand == null) return false;

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
