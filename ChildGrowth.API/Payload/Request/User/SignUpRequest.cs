using System.ComponentModel.DataAnnotations;
using ChildGrowth.API.Enums;

namespace ChildGrowth.API.Payload.Request.User;
public class SignUpRequest
{
    [Required]
    public RoleEnum UserType { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = null!;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [StringLength(10)]
    public string Gender { get; set; } = null!;
}
