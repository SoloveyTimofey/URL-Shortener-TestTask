namespace UrlShortener.BussinessLogic.Models;

public class ApplicationIdentityResult
{
    public bool Succeeded
    {
        get => Errors.Count()==0;
    }
    public List<string> Errors { get; set; } = [];
}