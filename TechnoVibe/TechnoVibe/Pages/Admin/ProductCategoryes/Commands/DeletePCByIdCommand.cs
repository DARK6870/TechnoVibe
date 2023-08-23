using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductCategoryes.Commands
{
    public record DeletePCByIdCommand(int id) : IRequest<bool>;

    public class DeletePCByIdHandler : IRequestHandler<DeletePCByIdCommand, bool>
    {
        private readonly AppDBContext _context;
        public DeletePCByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeletePCByIdCommand request, CancellationToken cancellationToken)
        {
            var pc = await _context.ProductCategories.FindAsync(request.id);
            if (pc == null) return false;

            _context.ProductCategories.Remove(pc);
            int res = await _context.SaveChangesAsync();

            return res > 0;
        }
    }
}
