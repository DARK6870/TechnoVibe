using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechnoVibe.Entity;
using TechnoVibe.Pages.Admin.SupportPage.Commands;
using WEB.Models;

namespace TechnoVibe.Pages.Admin.SupportPage
{
    [Authorize(Roles = "Admin, Manager")]
    public class SupportsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediatr;

        public SupportsController(AppDBContext context, IMediator mediatr)
        {
            _context = context;
            _mediatr = mediatr;
        }

        public async Task<IActionResult> IndexSupport()
        {
              return _context.Supports != null ? 
                          View(await _context.Supports.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Supports'  is null.");
        }

        public async Task<IActionResult> DeleteSupport(int id)
        {
            await _mediatr.Send(new DeleteSupportByIdCommand(id));

            return RedirectToAction("IndexSupport");
        }
    }
}
