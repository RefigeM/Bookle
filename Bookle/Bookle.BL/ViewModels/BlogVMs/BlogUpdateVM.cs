using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bookle.BL.ViewModels.BlogVMs
{
   public  class BlogUpdateVM
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }



        [Required(ErrorMessage = "File is required.")]
        public IFormFile File { get; set; }

        public string? FileUrl { get; set; }

        public DateTime UpdatedTime { get; set; } = DateTime.Now;
    }
}
