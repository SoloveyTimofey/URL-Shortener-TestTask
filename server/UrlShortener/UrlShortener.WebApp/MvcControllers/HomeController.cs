using Microsoft.AspNetCore.Mvc;
using UrlShortener.BussinessLogic.Services.ShortenUrl;
using UrlShortener.BussinessLogic.Dtos;

namespace UrlShortener.WebApp.MvcControllers;

public class HomeController : Controller
{
    private readonly IShortenUrlService _shortenUrlService;
    public HomeController(IShortenUrlService shortenUrlService)
    {
        _shortenUrlService = shortenUrlService;
    }

    public IActionResult Index() => View();

    [HttpGet("{shortenedUrl}")]
    public async Task<IActionResult> Index(string shortenedUrl)
    {
        var getFullUrlByShortenedDto = new GetFullUrlByShortenedDto {
            ShortenedUrl = shortenedUrl 
        };

        var fullUrl = await _shortenUrlService.GetFullUrlByShortenedAsync(getFullUrlByShortenedDto);

        return Redirect(fullUrl.OriginalUrl);
    }
}