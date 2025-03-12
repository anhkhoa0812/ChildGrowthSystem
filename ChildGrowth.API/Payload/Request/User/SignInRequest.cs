using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.User;

public class SignInRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = null!;
}