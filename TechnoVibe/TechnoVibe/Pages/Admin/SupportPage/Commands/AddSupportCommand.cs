using MediatR;
using TechnoVibe.Entity;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.SupportPage.Commands
{
	public record AddSupportCommand(Support support) : IRequest<Support>;

	public class AddSupportHandler : IRequestHandler<AddSupportCommand, Support>
	{
		private readonly AppDBContext _context;
		public AddSupportHandler(AppDBContext context)
		{
			_context = context;
		}

		public async Task<Support> Handle(AddSupportCommand request, CancellationToken cancellationToken)
		{
			var sup = request.support;

			await _context.Supports.AddAsync(sup);
			await _context.SaveChangesAsync();

			return sup;
		}
	}
}
