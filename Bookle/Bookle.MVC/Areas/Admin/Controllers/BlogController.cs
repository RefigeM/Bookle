using Bookle.BL.Extentions;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.AuthorVMs;
using Bookle.BL.ViewModels.BlogVMs;
using Bookle.Core.Entities;
using Bookle.DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
    public class BlogController(IBlogService _blogService, IWebHostEnvironment _env,BookleDbContext _context) : Controller
    {
		public async Task<IActionResult> Index()
        {
			var blogs = await _blogService.GetAllRecentPostsAsync(); // Buradakı kodun işləyişini yoxlayın

			if (blogs == null)
			{
				// Əgər burada `blogs` null olarsa, səhifəyə keçilməməli və ya xəbərdarlıq mesajı göstərilməlidir
				return View(new List<Blog>());
			}

			return View(blogs);
		}
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(BlogCreateVM vm)
		{
			if (vm.File != null)
			{
				if (!vm.File.IsValidType("image"))
					ModelState.AddModelError("File", "File must be an image");

				if (!vm.File.IsValidSize(400))
					ModelState.AddModelError("File", "File must be less than 400KB");
			}
			else
			{
				ModelState.AddModelError("File", "File is required");
			}

			if (!ModelState.IsValid)
			{
				return View(vm);
			}


			var imagePath = Path.Combine(_env.WebRootPath, "imgs", "blogs");

			string newFileName = await vm.File.UploadAsync(imagePath);

			Blog blog = new Blog
			{
				Title= vm.Title,	
				Content = vm.Content,
				ImageUrl= "/imgs/blogs/" + newFileName,
				CreatedDate= DateTime.Now

			};
			await _blogService.AddPostAsync(blog);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(int id) 
		{
            if (id == null) return BadRequest();
            var data = await _context.Blogs.Where(x => x.Id == id)
                .Select(x => new BlogUpdateVM
                {
                  
                    FileUrl = x.ImageUrl,
					Content = x.Content,	
					Title = x.Title
                }).FirstOrDefaultAsync();

            if (data == null) return NotFound();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, BlogUpdateVM vm)
        {
            await _blogService.UpdatePostAsync(id.Value, vm);

            return RedirectToAction(nameof(Index));

        }
		public async Task<IActionResult> Info(int? id) 
		{
			if (id == null) return BadRequest();
			var data = await _blogService.GetBlogByIdAsync(id.Value);
            if (data == null)
            {
				return NotFound();
            }
			return View(data);	
        }
		public async Task<IActionResult> Delete(int? id) 
		{
			if (id == null) return BadRequest();
			var commet = await _blogService.GetBlogByIdAsync(id.Value);
			await _blogService.DeletePostAsync(id.Value);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Hide(int? id)
		{
			if (id == null) return BadRequest();
			await _blogService.SoftDeketeAsync(id.Value);

			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Show(int? id)
		{
			if (id == null) return BadRequest();
			await _blogService.RestoreBlogAsync(id.Value);

			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> ToggleIsVisible(int? id) 
		{
		if(!id.HasValue) return BadRequest();	
            _blogService.ToggleIsVisible(id.Value);	
			return View(nameof(Index));	

		}


	}
}
