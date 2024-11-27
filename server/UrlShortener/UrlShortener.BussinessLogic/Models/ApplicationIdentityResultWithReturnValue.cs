namespace UrlShortener.BussinessLogic.Models;

public class ApplicationIdentityResultWithReturnValue : ApplicationIdentityResult
{
    public string? Token { get; set; } = null;
}
