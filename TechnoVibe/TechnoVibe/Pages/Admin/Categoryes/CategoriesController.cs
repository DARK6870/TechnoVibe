using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.Categoryes.Commands;
using TechnoVibe.Pages.Admin.Categoryes.Queryes;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.Categoryes
{
    [Authorize(Roles = "Admin, Manager")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediatr;
        private readonly IAppCache _cache;

        public CategoriesController(AppDBContext context, IMediator mediatr, IAppCache cache)
        {
            _mediatr = mediatr;
            _cache = cache;
        }


        public async Task<IActionResult> IndexCategory()
        {
            var products = await _cache.GetOrAddAsync("category_data", async () =>
            {
                var result = await _mediatr.Send(new GetAllCategoryesQuery());
                return result;
            }, TimeSpan.FromHours(2));

            return View(products);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (await _mediatr.Send(new AddNewCategoryCommand(category)))
            {
                _cache.Remove("category_data");
                return RedirectToAction(nameof(IndexCategory));
            }
            
            return Problem("Something went wrond");
            
        }

        public async Task<IActionResult> EditCategory(byte id)
        {
            try
            {
                var category = await _mediatr.Send(new GetCategoryByIdQuery(id));
                if (category != null)
                {
                    return View(category);
                }
                else return NotFound();
                
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(byte id, Category category)
        {
                try
                {
                await _mediatr.Send(new UpdateCategoryCommand(category));
                _cache.Remove("category_data");
                }
                catch (Exception ex)
                {
                return Problem(ex.Message);
                }
                return RedirectToAction("IndexCategory");
        }


        public async Task<IActionResult> DeleteCategory(byte id)
        {
            try
            {
                var res = await _mediatr.Send(new DeleteCategoryByIdCommand(id));
                _cache.Remove("category_data");
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            return RedirectToAction("IndexCategory");
        }
    }
}
