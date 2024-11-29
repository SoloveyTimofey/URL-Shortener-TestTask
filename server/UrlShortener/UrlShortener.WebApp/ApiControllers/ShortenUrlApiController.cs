using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortener.BussinessLogic.Dtos;
using UrlShortener.BussinessLogic.Services.ShortenUrl;
using UrlShortener.Infrastructure.Utils;

namespace UrlShortener.WebAPI.ApiControllers;

[ApiController, Route("api/[controller]")]
public class ShortenUrlApiController : ControllerBase
{
    private readonly IShortenUrlService _shortenUrlService;
    private readonly IMapper _mapper;
    public ShortenUrlApiController(IShortenUrlService shortenUrlService, IMapper mapper)
    {
        _shortenUrlService = shortenUrlService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet, Route(nameof(GetShortenedUrls))]
    public async Task<IActionResult> GetShortenedUrls()
    {
        var shortendUrlItems = await _shortenUrlService.GetAllShortenedUrlsAsync();

        return Ok(shortendUrlItems);
    }

    [HttpGet, Route(nameof(GetShortenedUrlsForUnathorizedUsers))]
    public async Task<IActionResult> GetShortenedUrlsForUnathorizedUsers()
    {
        var shortendUrlItems = await _shortenUrlService.GetAllShortenedUrlsForUnathorizedUsersAsync();

        return Ok(shortendUrlItems);
    }

    [Authorize]
    [HttpPost, Route(nameof(CreateShortenedUrl))]
    public async Task<IActionResult> CreateShortenedUrl(string originalUrl)
    {
        var shortenedUrlCreateDto = new ShortenedUrlCreateDto
        {
            OriginalUrl = originalUrl,
            CreatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!
        };

        var createdUrl = await _shortenUrlService.CreateShortenedUrlAsync(shortenedUrlCreateDto);

        return Ok(_mapper.Map<ShortenedUrlReadDto>(createdUrl));
    }

    [HttpGet, Route(nameof(GetFullUrlByShortened))]
    public async Task<IActionResult> GetFullUrlByShortened(GetFullUrlByShortenedDto getFullUrlByShortenedDto)
    {
        var fullUrl = await _shortenUrlService.GetFullUrlByShortenedAsync(getFullUrlByShortenedDto);

        if (User.IsInAnyRole())
        {
            return Ok(_mapper.Map<ShortenedUrlReadDto>(fullUrl));
        }

        return Ok(_mapper.Map<ShortenedUrlReadForUnauthorizedUsersDto>(fullUrl));
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