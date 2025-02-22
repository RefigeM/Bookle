using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bookle.BL.ViewModels.AuthorVMs;

public class AuthorUpdateVM
{
    [Required]
    [MaxLength(32, ErrorMessage = "Fulname 32den cox ola bilmez")]
    public string AuthorName { get; set; }
    [Required]
    public string? FileUrl { get; set; }
    [Required]
    public IFormFile File { get; set; }
    public string? Biography { get; set; }
    public int? BirthYear { get; set; }
    public int? DeathYear { get; set; }
    public string? Country { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? LinkedInUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BirthYear.HasValue && DeathYear.HasValue)
        {
            if (BirthYear.Value > DeathYear.Value)
            {
                yield return new ValidationResult(
                    "Birth year cannot be greater than death year.",
                    new[] { nameof(BirthYear), nameof(DeathYear) });
            }
        }

    }
}