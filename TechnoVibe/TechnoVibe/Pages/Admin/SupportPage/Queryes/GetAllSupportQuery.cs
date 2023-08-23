using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.SupportPage.Queryes
{
    public record GetAllSupportQuery() : IRequest<List<Support>>;

    public class GetAllSupportHandler : IRequestHandler<GetAllSupportQuery, List<Support>>
    {
        private readonly AppDBContext _context;
        public GetAllSupportHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Support>> Handle(GetAllSupportQuery request, CancellationToken cancellationToken)
        {
            var res = await _context.Supports.ToListAsync();
            return res;
        }
    }
}
