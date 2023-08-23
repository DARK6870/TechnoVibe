using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Brands.Commands
{
    public record AddNewBrandCommand(Brand newBrand) : IRequest<bool>;

    public class AddNewBrandHandler : IRequestHandler<AddNewBrandCommand, bool>
    {
        private readonly AppDBContext _context;
        public AddNewBrandHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddNewBrandCommand request, CancellationToken cancellationToken)
        {
            await _context.Brands.AddAsync(request.newBrand);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
