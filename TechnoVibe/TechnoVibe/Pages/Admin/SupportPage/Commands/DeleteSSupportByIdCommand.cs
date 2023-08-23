using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.SupportPage.Commands
{
    public record DeleteSupportByIdCommand(int id) : IRequest<Support>;

    public class DeleteSupportByIdHandler : IRequestHandler<DeleteSupportByIdCommand, Support>
    {
        private readonly AppDBContext _context;
        public DeleteSupportByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Support> Handle(DeleteSupportByIdCommand request, CancellationToken cancellationToken)
        {
            var sup = await _context.Supports.FindAsync(request.id);

                _context.Supports.Remove(sup);
                await _context.SaveChangesAsync();

                return sup;
        }
    }
}
