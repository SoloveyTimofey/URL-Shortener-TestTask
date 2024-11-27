using System.ComponentModel.DataAnnotations;

namespace URLShortener.WebAPI.Models
{
    public sealed class AddOrUpdateApplicationUserModel
    {
        [Required(ErrorMessage = "User name is required.")] public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "EmailIsRequired.")] public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")] public string Password { get; set; } = string.Empty;
    }
}
