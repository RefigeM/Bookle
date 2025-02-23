using Bookle.BL.Services.Implements;
using Bookle.BL.Services.Interfaces;
using Bookle.BL.ViewModels.FilterVMs;
using Bookle.Core.Enums;
using Bookle.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Bookle.MVC.Controllers
{
    
    public class FilterController : Controller
    {
        private readonly IBookService _service;
		private readonly IAuthorService _authorService;
        private readonly IBookRepository _book;


		public FilterController(IBookService service, IBookRepository book)
        {
            _service = service;
            _book = book;   
        }



        public async Task<IActionResult> Index(int? page = 1, int? take = 4)
        {
            if (!page.HasValue) page = 1;
            if (!take.HasValue) take = 4;

            var query = _book.GetAllBooksWithDetails();

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
