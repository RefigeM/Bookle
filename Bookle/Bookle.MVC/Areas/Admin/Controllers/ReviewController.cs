using Bookle.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ReviewController : Controller
	{
		private readonly ICommentService _service;

		public ReviewController(ICommentService service)
		{
			_service = service;	
		}

		public async Task<IActionResult> Index()
		{
			return View(await _service.GetCommentWithBookAndUser());
		}

		public async Task<IActionResult> Delete(int? id) 
		{
			if (id == null) return BadRequest();
			var commet = await _service.GetCommentByIdAsync(id.Value);
			await _service.DeleteCommentAsync(id.Value);
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Info(int? id) 
		{
			if (id == null) return BadRequest();
			var comment = await _service.GetCommentIdtWithDetailsAsync(id.Value);
			return View(comment);
		}


	}
}
