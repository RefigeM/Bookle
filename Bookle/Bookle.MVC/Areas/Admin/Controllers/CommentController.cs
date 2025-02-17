﻿using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.BookVMs;
using Bookle.BL.ViewModels.CommentVMs;
using Bookle.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookle.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommentController: Controller	
	{
		private readonly ICommentService _service;
		private readonly IBookService _bookService;
		private readonly IUserService _userService;

		public CommentController(ICommentService service, IBookService bookService, IUserService userService)
		{
			_service = service;	
			_bookService = bookService;	
			_userService = userService;	

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
		public async Task<IActionResult> Update(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}

			var comment = await _service.GetCommentByIdAsync(id);

			// Books və Users məlumatlarını asinxron şəkildə əldə edirik
			var books = await _bookService.GetAllBooksAsync();
			var users = await _userService.GetAllUsersAsync();

			// Books və Users-in null olub-olmadığını yoxlayırıq
			if (books == null || users == null)
			{
				// Burada hər hansı bir səhv mesajı göstərmək olar
				return View("Error");  // Səhv səhifəsi göstərmək
			}

			// ViewBag vasitəsilə məlumatları göndəririk
			ViewBag.Books = new SelectList(books, "Id", "Title", comment.BookId);  // `BookId`-ni seçilmiş olaraq göstəririk
			ViewBag.Authors = new SelectList(users, "Id", "UserName", comment.UserId); // `UserId`-ni seçilmiş olaraq göstəririk

			if (comment == null)
			{
				return NotFound();
			}

			var model = new CommentUpdateVM
			{
				BookId = comment.BookId,
				UserId = comment.UserId,
				Content = comment.Content
			};

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int? id, CommentUpdateVM vm)
		{
			if (id == null) return BadRequest();

			var comment = await _service.GetCommentByIdAsync(id.Value);
			if (comment == null || comment.IsDeleted)
			{
				ModelState.AddModelError("Comment", "The selected author is either deleted or does not exist.");
				return View(vm);
			}

			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			await _service.UpdateCommentAsync(id.Value, vm);

			return RedirectToAction(nameof(Index));

		}




	}
}
