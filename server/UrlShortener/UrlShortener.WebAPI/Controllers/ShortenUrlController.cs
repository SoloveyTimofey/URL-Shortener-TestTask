using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BussinessLogic.Dtos;
using UrlShortener.BussinessLogic.Services.ShortenUrl;

namespace UrlShortener.WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public class ShortenUrlController : ControllerBase
{
    private readonly IShortenUrlService _shortenUrlService;
    private readonly IMapper _mapper;
    public ShortenUrlController(IShortenUrlService shortenUrlService, IMapper mapper)
    {
        _shortenUrlService = shortenUrlService;
        _mapper = mapper;
    }

    [HttpGet, Route(nameof(GetShortenedUrls))]
    public async Task<IActionResult> GetShortenedUrls()
    {
        var shortenUrlItems = await _shortenUrlService.GetAllShortenedUrlsAsync();

        return Ok(_mapper.Map<IEnumerable<ShortenedUrlReadDto>>(shortenUrlItems));
    }

    [Authorize]
    [HttpPost, Route(nameof(CreateShortenedUrl))]
    public async Task<IActionResult> CreateShortenedUrl(ShortenedUrlCreateDto shortenedUrlCreateDto)
    {
        var createdUrl = await _shortenUrlService.CreateShortenedUrlAsync(shortenedUrlCreateDto);

        return Ok(_mapper.Map<ShortenedUrlReadDto>(createdUrl));
    }

    [Authorize]
    [HttpPatch, Route(nameof(ChangeOriginalUrl))]
    public async Task<IActionResult> ChangeOriginalUrl(ChangeOriginalUrlDto changeOriginalUrlDto)
    {
        var changedUrl = await _shortenUrlService.ChangeOriginalUrlAsync(changeOriginalUrlDto, "sss");

        return Ok(_mapper.Map<ShortenedUrlReadDto>(changedUrl));
    }

    [Authorize]
    [HttpDelete, Route(nameof(DeleteShortenedUrl))]
    public async Task<IActionResult> DeleteShortenedUrl(uint id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        await _shortenUrlService.RemoveShortenedUrlAsync(id,"dd");

        return Ok();
    }
}