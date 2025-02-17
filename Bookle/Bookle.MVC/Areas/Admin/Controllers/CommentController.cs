using Bookle.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommentController(ICommentService _service) : Controller
	{
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
		[HttpGet]
		public async Task<IActionResult> Update(int? id)
		{
			return View();
		}

	}
}
