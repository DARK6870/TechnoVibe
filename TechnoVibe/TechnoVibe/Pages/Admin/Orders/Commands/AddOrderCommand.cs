using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Orders.Commands
{
    public record AddOrderCommand(Order order) : IRequest<Order>;

    public class AddOrderHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly AppDBContext _context;
        public AddOrderHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var ord = request.order;

            await _context.Orders.AddAsync(ord);
            await _context.SaveChangesAsync();

            return ord;
        }
    }
}
