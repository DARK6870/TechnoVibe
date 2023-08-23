using MediatR;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Orders.Queryes
{
    public record GetAllOrdersQuery() : IRequest<List<Order>>;

    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<Order>>
    {
        private readonly AppDBContext _context;
        public GetAllOrdersHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var res = await _context.Orders.Include(c => c.Statuss).Include(p => p.Products).ThenInclude(p => p.Brands).ToListAsync();

            return res;
        }
    }
}
