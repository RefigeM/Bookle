using Bookle.BL.Services.Implements;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.FilterVMs;
using Bookle.Core.Enums;
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
            var model = _service.GetBooksByGenre(null); // Bütün kitabları və janrları gətirir
            return View(model);
        }

        public IActionResult Search(string query)
        {
            var books = _service.Search(query);
            return View(books);
        }
        public IActionResult FilterByGenre(string genre)
        {
            if (!Enum.TryParse(genre, out Genre selectedGenre))
            {
                return NotFound(); // Səhv dəyər gəlsə, səhifə tapılmasın.
            }

            var model = _service.GetBooksByGenre(selectedGenre);
            model.SelectedGenre = selectedGenre; // View üçün seçilmiş janrı əlavə et
            return View(model);
        }


    }
}
