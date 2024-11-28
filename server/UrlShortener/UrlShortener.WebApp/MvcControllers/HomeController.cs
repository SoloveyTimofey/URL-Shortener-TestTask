using Microsoft.AspNetCore.Mvc;
using UrlShortener.BussinessLogic.Services.ShortenUrl;

namespace UrlShortener.WebApp.MvcControllers;

public class HomeController : Controller
{
    private readonly IShortenUrlService _shortenUrlService;
    public HomeController(IShortenUrlService shortenUrlService)
    {
        _shortenUrlService = shortenUrlService;
    }

    public IActionResult Index() => View();
}