using Bookle.BL.Services.Interfaces;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class UserController(BookleDbContext _context, IWebHostEnvironment _env, IAuthorService _service) : Controller
    {
        public async Task<IActionResult> Index()
        {           
            return View(await _context.Users.ToListAsync());
        }
    }
}
