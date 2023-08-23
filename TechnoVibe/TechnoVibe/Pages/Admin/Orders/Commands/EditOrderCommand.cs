using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Orders.Commands
{
    public record EditOrderCommand(Order order) : IRequest<bool>;

    public class EditOrderHandler : IRequestHandler<EditOrderCommand, bool>
    {
        private readonly AppDBContext _context;
        public EditOrderHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var ord = request.order;

            _context.Orders.Update(ord);
            int res = await _context.SaveChangesAsync();

            return res > 0;
        }
    }
}
