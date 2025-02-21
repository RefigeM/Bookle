using Bookle.BL.Services.Implements;
using Bookle.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookle.MVC.Controllers
{
    
    public class FilterController : Controller
    {
        private readonly IBookService _service;

        public FilterController(IBookService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(string query)
        {
            var books = _service.Search(query);
            return View(books);
        }
    }
}
