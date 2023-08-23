using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.Brands.Commands;
using TechnoVibe.Pages.Admin.Brands.Queryes;
using TechnoVibe.Pages.Admin.Categoryes.Commands;
using TechnoVibe.Pages.Admin.Categoryes.Queryes;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Brands
{
    [Authorize(Roles = "Admin, Manager")]
    public class BrandController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediatr;
        private readonly IAppCache _cache;

        public BrandController(AppDBContext context, IMediator mediatr, IAppCache cache)
        {
            _context = context;
            _mediatr = mediatr;
            _cache = cache;
        }

        public async Task<IActionResult> IndexBrand()
        {
            var brands = await _mediatr.Send(new GetAllBrandsQuery());

            return View(brands);
        }

        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBrand(Brand brand)
        {
            if (await _mediatr.Send(new AddNewBrandCommand(brand)))
            {
                return RedirectToAction(nameof(IndexBrand));
            }

            return Problem("Something went wrond");

        }

        public async Task<IActionResult> EditBrand(short id)
        {
            try
            {
                var brand = await _mediatr.Send(new GetBrandByIdQuery(id));
                if (brand != null)
                {
                    return View(brand);
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
        public async Task<IActionResult> EditBrand(short id, Brand brand)
        {
            try
            {
                await _mediatr.Send(new UpdateBrandCommand(brand));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return RedirectToAction("IndexBrand");
        }


        public async Task<IActionResult> DeleteBrand(short id)
        {
            try
            {
                var res = await _mediatr.Send(new DeleteBrandByIdCommand(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return RedirectToAction("IndexBrand");
        }
    }
}
