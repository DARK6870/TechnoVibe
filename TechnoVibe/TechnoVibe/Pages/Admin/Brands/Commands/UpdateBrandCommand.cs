using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Brands.Commands
{
    public record UpdateBrandCommand(Brand updated) : IRequest<bool>;

    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly AppDBContext _context;
        public UpdateBrandHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            _context.Brands.Update(request.updated);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
