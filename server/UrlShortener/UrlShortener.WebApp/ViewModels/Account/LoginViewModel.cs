using System.ComponentModel.DataAnnotations;

namespace UrlShortener.WebApp.ViewModels.Account;

public class LoginViewModel
{
    [Required] public required string Email { get; set; }
    [Required] public required string Password { get; set; }
}