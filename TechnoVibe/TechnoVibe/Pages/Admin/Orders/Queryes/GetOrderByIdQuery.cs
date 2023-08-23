using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Orders.Queryes
{
    public record GetOrderByIdQuery(int id) : IRequest<Order>;

    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly AppDBContext _context;
        public GetOrderByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _context.Orders.FindAsync(request.id);

            return res;
        }
    }
}
