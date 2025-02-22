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
        private readonly IAuthorService _authorService;

        public FilterController(IBookService service)
        {
            _service = service;
        }



        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllBooksWithDetailsAsync());

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

        public IActionResult FilterByFormat(string format)
        {
            if (!Enum.TryParse(format, out Format selectedFormat))
            {
                return NotFound(); // Səhv dəyər gəlsə, səhifə tapılmasın.
            }

            var model = _service.GetBooksByFormat(selectedFormat);
            model.SelectedFormat = selectedFormat; // View üçün seçilmiş janrı əlavə et
            return View(model);
        }
        public async Task<IActionResult> FilterByAuthor(string authorName)
        {
            //if (string.IsNullOrWhiteSpace(authorName))
            //{
            //    return RedirectToAction("Index"); // Əgər authorName boşdursa, əsas səhifəyə qaytar
            //}

            var books = await _service.GetAllBooksWithDetailsAsync();
            var authors = await _authorService.GetAllAuthorsAsync();
          
            var filteredBooks = books.Where(b => b.Author.AuthorName == authorName).ToList();

            if (!filteredBooks.Any())
            {
                ViewBag.Message = "No books found for this author.";
            }

            var model = new AuthorBooksVM
            {
                Books = filteredBooks, // Filter olunmuş kitablar
                Authors = authors.ToList()
            };

            return View(model); // "Shop" əvəzinə uyğun View adı istifadə et
        }





    }
}
