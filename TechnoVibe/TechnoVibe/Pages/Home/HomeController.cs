using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.SupportPage.Commands;

namespace TechnoVibe.Pages.Home
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediatr;

        public HomeController(ILogger<HomeController> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Support()  
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostSupport(Support support)
        {
            if (ModelState.IsValid)
            {
                await _mediatr.Send(new AddSupportCommand(support));
                return Redirect("SupportSucces");
            }
            return Redirect("Support");
        }

        public IActionResult SupportSucces()
        {
            return View();
        }

	}
}