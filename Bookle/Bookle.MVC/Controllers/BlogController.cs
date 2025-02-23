using Bookle.BL.Services.Interfaces;
using Bookle.Core.Entities;
using Bookle.Core.Repositories;
using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Controllers
{

    public class BlogController : Controller
    {
		private readonly IBlogService _blogServie;
		private readonly IBlogRepository _blogRepo;

		public BlogController(IBlogService blogServie, IBlogRepository blogRepo)
        {
            _blogServie = blogServie;
			_blogRepo = blogRepo;	
        }

		
		public async Task<IActionResult> Index(int? page = 1, int? take = 4)
		{
			if (!page.HasValue) page = 1;
			if (!take.HasValue) take = 4;

			var query = _blogRepo.GetAllPostsVisiblePostsAsync();

			decimal bookCount = await query.CountAsync();

			var data = await query
				.Skip(take.Value * (page.Value - 1))
				.Take(take.Value)
				.ToListAsync();

			decimal pageCount = Math.Ceiling(bookCount / (decimal)take.Value);
			ViewBag.PageCount = pageCount;
			ViewBag.Take = take;
			ViewBag.AktivePage = page;

			return View(data);
		}
		public async Task<IActionResult> Details(int? id) 
        {
            if (!id.HasValue) return BadRequest();
            var blog=await _blogServie.GetBlogByIdAsync(id.Value);   
            if (blog == null) return NotFound();    
        return View(blog);      
        }

    }
}
