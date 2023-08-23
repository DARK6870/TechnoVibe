using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.Brands.Commands;
using TechnoVibe.Pages.Admin.Brands.Queryes;
using TechnoVibe.Pages.Admin.Categoryes.Commands;
using TechnoVibe.Pages.Admin.Categoryes.Queryes;
using TechnoVibe.Pages.Admin.ProductPage.Commands;
using TechnoVibe.Pages.Admin.ProductPage.Models;
using TechnoVibe.Pages.Admin.ProductPage.Queryes;
using TechnoVibe.Pages.Products.Queryes;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.ProductPage
{
    [Authorize(Roles = "Admin, Manager")]
    public class AdminProductsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediatr;
        private readonly IAppCache _cache;

        public AdminProductsController(AppDBContext context, IMediator mediatr, IAppCache cache)
        {
            _context = context;
            _mediatr = mediatr;
            _cache = cache;
        }

        public async Task<IActionResult> CreateProduct()
        {
            var model = new NewProduct
            {
                Brands = await _mediatr.Send(new GetAllBrandsQuery())
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProducts(Product product)
        {
            if (await _mediatr.Send(new AddNewProductCommand(product)))
            {
                _cache.Remove("products_data");
                return RedirectToAction("AllProducts", "Product");
            }

            return Problem("Something went wrond");

        }

        public async Task<IActionResult> EditProduct(int id)
        {
            try
            {
                var product = await _mediatr.Send(new GetProductByIdQuery(id));
                if (product != null)
                {
                    return View(product);
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
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            try
            {
                await _mediatr.Send(new UpdateProductCommand(product));
                _cache.Remove("products_data");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return RedirectToAction("AllProducts", "Product");
        }


        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var res = await _mediatr.Send(new DeleteProductByIdCommand(id));
                _cache.Remove("products_data");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return RedirectToAction("AllProducts", "Product");
        }
    }
}
