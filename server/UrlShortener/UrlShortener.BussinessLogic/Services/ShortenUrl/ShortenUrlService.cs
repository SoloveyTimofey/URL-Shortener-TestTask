using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrlShortener.BussinessLogic.Dtos;
using UrlShortener.BussinessLogic.Utils;
using UrlShortener.DataAccess.Models;
using UrlShortener.DataAccess.Repositories.ShortenedUrlRepository;
using UrlShortener.Infrastructure.Constants;

namespace UrlShortener.BussinessLogic.Services.ShortenUrl;

internal class ShortenUrlService : IShortenUrlService
{
    private readonly IShortenedUrlRepository _shortenedUrlRepository;
    private readonly IMapper _mapper;
    public ShortenUrlService(IShortenedUrlRepository shortenedUrlRepository, IMapper mapper)
    {
        _shortenedUrlRepository = shortenedUrlRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<ShortenedUrlReadDto>> GetAllShortenedUrlsAsync()
    {
        var shortenedUrls = _shortenedUrlRepository.GetAllShortenedUrls();

        var shortenedUrlsWithFullInfo = shortenedUrls
            .Include(url => url.Creator)
            .Select(url => new ShortenedUrlReadDto
            {
                Id = url.Id,
                CreatedAt = url.CreatedAt,
                CreatorId = url.CreatorId,
                OriginalUrl = url.OriginalUrl,
                Shortened = url.Shortened,
                CreatorName = url.Creator!.UserName!
            });

        return await shortenedUrlsWithFullInfo.ToListAsync();
    }

    public async Task<ICollection<ShortenedUrlReadForUnauthorizedUsersDto>> GetAllShortenedUrlsForUnathorizedUsersAsync()
    {
        var shortenedUrls = _shortenedUrlRepository.GetAllShortenedUrls();

        var shortenedUrlsForUnathorized = shortenedUrls.Select(url => new ShortenedUrlReadForUnauthorizedUsersDto
        {
            Id = url.Id,
            Shortened = url.Shortened,
            OriginalUrl = url.OriginalUrl,
        });

        return await shortenedUrlsForUnathorized.ToListAsync();
    }

    public async Task<ShortenedUrl> GetFullUrlByShortenedAsync(GetFullUrlByShortenedDto getFullUrlByShortenedDto)
    {
        var shortenedUrls = _shortenedUrlRepository.GetAllShortenedUrls();

        var targetUrl = await shortenedUrls.FirstAsync(shortenedUrl=>shortenedUrl.Shortened == getFullUrlByShortenedDto.ShortenedUrl)
            ??  throw new ApplicationException(ExceptionMessages.FullUrlWithSpecifiedShortenedVersionNotFound);

        return targetUrl;
    }

    public async Task<ShortenedUrl> CreateShortenedUrlAsync(ShortenedUrlCreateDto shortenedUrlCreateDto)
    {
        var shortenedUrl = _mapper.Map<ShortenedUrl>(shortenedUrlCreateDto);

        shortenedUrl.Shortened = ShortenedUrlGenerator.ShortenUrl(shortenedUrl.OriginalUrl);

        var createdUrl = await _shortenedUrlRepository.CreateShortenedUrlAsync(shortenedUrl);

        await _shortenedUrlRepository.SaveChangesAsync();

        return createdUrl;
    }

    public async Task<ShortenedUrl> ChangeOriginalUrlAsync(ChangeOriginalUrlDto chnageOriginalUrlDto, string executorId)
    {
        var shortenedUrl = _mapper.Map<ShortenedUrl>(chnageOriginalUrlDto);

        shortenedUrl.Shortened = ShortenedUrlGenerator.ShortenUrl(shortenedUrl.OriginalUrl);

        var updatedUrl = await _shortenedUrlRepository.UpdateShortenedUrlAsync(shortenedUrl);

        await _shortenedUrlRepository.SaveChangesAsync();

        return updatedUrl;
    }

    public async Task RemoveShortenedUrlAsync(uint id, string executorId)
    {
        _shortenedUrlRepository.RemoveShortenedUrl(id);

        await _shortenedUrlRepository.SaveChangesAsync();
    }
}