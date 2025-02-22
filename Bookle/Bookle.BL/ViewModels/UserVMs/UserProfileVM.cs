using System.ComponentModel.DataAnnotations;

namespace Bookle.BL.ViewModels.UserVMs;

public class UserProfileVM
{
    public string UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

  

    [EmailAddress]
    public string Email { get; set; }

    public string? Address { get; set; }

    public string ProfileImageUrl { get; set; }  

}
