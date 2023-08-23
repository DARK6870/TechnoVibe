using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.Categoryes.Commands;
using TechnoVibe.Pages.Admin.Categoryes.Queryes;
using TechnoVibe.Pages.Admin.ProductCategoryes.Commands;
using TechnoVibe.Pages.Admin.ProductCategoryes.Models;
using TechnoVibe.Pages.Admin.ProductCategoryes.Queryes;
using TechnoVibe.Pages.Admin.ProductPage.Queryes;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductCategoryes
{
    [Authorize(Roles = "Admin, Manager")]
    public class ProductCat : Controller
    {

        private readonly IMediator _mediatr;
        private readonly IAppCache _cache;
        public ProductCat(IMediator mediatr, IAppCache cache)
        {
            _mediatr = mediatr;
            _cache = cache;
        }
        public async Task<IActionResult> IndexPC()
        {
            var PC = await _mediatr.Send(new GetAllPCQuery());
            return View(PC);
        }

        public async Task<IActionResult> CreatePC()
        {
            var model = new NewPC();
            model.Products = await _mediatr.Send(new GetAllProductsQuery());
            model.Categories = await _mediatr.Send(new GetAllCategoryesQuery());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePC(ProductCategory category)
        {
            if (await _mediatr.Send(new AddNewPCCommand(category)))
            {
                _cache.Remove("products_data");
                return RedirectToAction(nameof(IndexPC));
            }

            return Problem("Something went wrond");

        }

        public async Task<IActionResult> DeletePC(int id)
        {
            try
            {
                var res = await _mediatr.Send(new DeletePCByIdCommand(id));
                _cache.Remove("products_data");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return RedirectToAction("IndexPC");
        }
    }
}

