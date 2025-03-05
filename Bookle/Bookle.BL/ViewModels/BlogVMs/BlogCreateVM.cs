using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bookle.BL.ViewModels.BlogVMs;

public class BlogCreateVM
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(64, ErrorMessage = "Content must be less than 64.")]

    public string Title { get; set; }

    [Required(ErrorMessage = "Content is required.")]
    [MaxLength(4000, ErrorMessage = "Content must be less than 4000.")]
    public string Content { get; set; }

 

    [Required(ErrorMessage = "File is required.")]
    public IFormFile File { get; set; }

    public string? FileUrl { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
