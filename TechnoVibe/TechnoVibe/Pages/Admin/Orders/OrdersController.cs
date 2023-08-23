using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.Categoryes.Commands;
using TechnoVibe.Pages.Admin.Categoryes.Queryes;
using TechnoVibe.Pages.Admin.Orders.Commands;
using TechnoVibe.Pages.Admin.Orders.Queryes;
using TechnoVibe.Pages.Admin.ProductCategoryes.Commands;
using TechnoVibe.Pages.Admin.ProductCategoryes.Queryes;
using TechnoVibe.Pages.Admin.ProductPage.Commands;
using TechnoVibe.Pages.Admin.ProductPage.Models;
using TechnoVibe.Pages.Products.Queryes;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Orders
{
    [Authorize(Roles = "Admin, Manager, Driver")]
    public class OrdersController : Controller
    {

        private readonly IMediator _mediatr;
        public OrdersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task<IActionResult> IndexOrder()
        {
            var orders = await _mediatr.Send(new GetAllOrdersQuery());
            return View(orders);
        }

        public async Task<IActionResult> EditOrder(int id)
        {
            try
            {
                var order = await _mediatr.Send(new GetOrderByIdQuery(id));
                if (order != null)
                {
                    return View(order);
                }
                else return NotFound();

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrder(int id, Order order)
        {
            try
            {
                await _mediatr.Send(new EditOrderCommand(order));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return RedirectToAction("IndexOrder", "Orders");
        }

    }
}

