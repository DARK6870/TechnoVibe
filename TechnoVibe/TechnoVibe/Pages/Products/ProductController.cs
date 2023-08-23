using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.Brands.Queryes;
using TechnoVibe.Pages.Admin.Categoryes.Queryes;
using TechnoVibe.Pages.Admin.Orders.Commands;
using TechnoVibe.Pages.Admin.ProductPage.Commands;
using TechnoVibe.Pages.Admin.ProductPage.Queryes;
using TechnoVibe.Pages.Products.Models;
using TechnoVibe.Pages.Products.Queryes;
using WEB.Models;

namespace TechnoVibe.Pages.Products
{
	public class ProductController : Controller
	{
		private readonly IAppCache _cache;
        private readonly IMediator _mediatr;

		public ProductController(IAppCache cache, IMediator mediatr)
		{
			_cache = cache;
            _mediatr = mediatr;
		}

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _mediatr.Send(new GetProductByIdQuery(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryProducts(string categoryName, List<string> selectedBrands, List<string> selectedCategories,  string sortOption, int page = 1)
        {
            ViewBag.selectedBrands = selectedBrands;
            ViewBag.selectedCategories = selectedCategories;
            ViewBag.sortOption = sortOption;

            var productsQuery = await _mediatr.Send(new GetProductByCategoryQuery(categoryName));

            if (selectedBrands.Count > 0)
            {
                productsQuery = productsQuery.Where(x => selectedBrands.Contains(x.Brands.BrandName)).ToList();
            }
            if (selectedCategories.Count > 0)
            {
                productsQuery = productsQuery.Where(x => x.ProductCategories.Any(pc => selectedCategories.Contains(pc.Categories.CategoryName))).ToList();
            }

            if (!string.IsNullOrEmpty(sortOption))
            {
                if (sortOption == "lowToHigh")
                {
                    productsQuery = productsQuery.OrderBy(x => x.Price).ToList();
                }
                else if (sortOption == "highToLow")
                {
                    productsQuery = productsQuery.OrderByDescending(x => x.Price).ToList();
                }
                else if (sortOption == "A-Z")
                {
                    productsQuery = productsQuery.OrderBy(x => x.ProductName).ToList();
                }
                else if (sortOption == "Z-A")
                {
                    productsQuery = productsQuery.OrderByDescending(x => x.ProductName).ToList();
                }
                else
                {

                }
            }

            int pageSize = 15;
            int skip = (page - 1) * pageSize;
            var product = productsQuery.Skip(skip).Take(pageSize).ToList();


            int totalProducts = productsQuery.Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);


            AllProductModel products = new AllProductModel()
            {
                Products = product,
                Brands = await _mediatr.Send(new GetBrandByCategoryNameQuery(categoryName)),
                Categories = await _mediatr.Send(new GetCategoryByNameQuery(categoryName)),
                categoryName = categoryName,
                actionName = "/Product/CategoryProducts",
                PageNumber = page,
                TotalPages = totalPages,
            };

            return View("AllProducts", products);
        }

        [HttpGet]
        public async Task<IActionResult> AllProducts(List<string> selectedBrands, List<string> selectedCategories, string sortOption, int page = 1)
        {
            ViewBag.selectedBrands = selectedBrands;
            ViewBag.selectedCategories = selectedCategories;
            ViewBag.sortOption = sortOption;

            List<string> category = selectedCategories ?? new List<string>();

            var productsQuery = await _cache.GetOrAddAsync("products_data", async () =>
            {
                var result = await _mediatr.Send(new GetAllProductsQuery());
                return result;
            }, TimeSpan.FromHours(1));


            if (selectedBrands.Count > 0)
            {
                productsQuery = productsQuery.Where(x => selectedBrands.Contains(x.Brands.BrandName)).ToList();
            }

            if (selectedCategories.Count > 0)
            {
                productsQuery = productsQuery.Where(x => x.ProductCategories.Any(pc => selectedCategories.Contains(pc.Categories.CategoryName))).ToList();
            }

            if (!string.IsNullOrEmpty(sortOption))
            {
                if (sortOption == "lowToHigh")
                {
                    productsQuery = productsQuery.OrderBy(x => x.Price).ToList();
                }
                else if (sortOption == "highToLow")
                {
                    productsQuery = productsQuery.OrderByDescending(x => x.Price).ToList();
                }
                else if (sortOption == "A-Z")
                {
                    productsQuery = productsQuery.OrderBy(x => x.ProductName).ToList();
                }
                else if (sortOption == "Z-A")
                {
                    productsQuery = productsQuery.OrderByDescending(x => x.ProductName).ToList();
                }
                else
                {

                }
            }

            int pageSize = 15;
            int skip = (page - 1) * pageSize;
            var product = productsQuery.Skip(skip).Take(pageSize).ToList();


            int totalProducts = productsQuery.Count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);


            AllProductModel products = new AllProductModel()
            {
                Products = product,
                Brands = await _mediatr.Send(new GetAllBrandsQuery()),
                Categories = await _mediatr.Send(new GetAllCategoryesQuery()),
                categoryName = "All Products",
                actionName = "/Product/AllProducts",
                PageNumber = page,
                TotalPages = totalPages
            };

            return View(products);
        }



        [HttpPost]
        public async Task<IActionResult> Order(string phoneNumber, string address, string fullName, int productId, decimal total, short qua)
        {
            if (qua > 0)
            {
                var newOrder = new Order
                {
                    phoneNumber = phoneNumber,
                    Address = address,
                    FullName = fullName,
                    ProductId = productId,
                    TotalPrice = total,
                    StatusId = 1,
                    AppUserId = "1",
                };
                await _mediatr.Send(new AddOrderCommand(newOrder));

                var pr = await _mediatr.Send(new GetProductByIdQuery(productId));

                if (pr != null)
                {
                    pr.Quanity -= 1;
                    await _mediatr.Send(new UpdateProductCommand(pr));
                }
            }
            else
            {
                return RedirectToAction("NotAvalible", "Product");
            }

            return RedirectToAction("Succes", "Product", new { id = productId });
        }

        public IActionResult Succes()
        {
            return View();
        }
        
        public IActionResult NotAvalible()
        {
            return View();
        }
    }
}
