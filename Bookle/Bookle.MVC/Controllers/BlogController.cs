using Bookle.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Controllers
{

    public class BlogController : Controller
    {
        private readonly IBlogService _blogServie;

        public BlogController(IBlogService blogServie)
        {
            _blogServie = blogServie;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogServie.GetAllPostsVisiblePostsAsync();
            return View(blogs);
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
