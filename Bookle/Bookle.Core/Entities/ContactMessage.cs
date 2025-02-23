using Bookle.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Bookle.Core.Entities;

public class ContactMessage :BaseEntity
{
    public string Name { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, StringLength(500)]
    public string Message { get; set; } = null!;
}
