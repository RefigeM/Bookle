﻿using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
