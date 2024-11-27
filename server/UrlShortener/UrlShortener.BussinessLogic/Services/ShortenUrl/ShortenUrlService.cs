using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UrlShortener.BussinessLogic.Dtos;
using UrlShortener.BussinessLogic.Utils;
using UrlShortener.DataAccess.Models;
using UrlShortener.DataAccess.Repositories;

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

    public async Task<ICollection<ShortenedUrl>> GetAllShortenedUrlsAsync() =>
        await _shortenedUrlRepository.GetAllShortenedUrls().ToListAsync();

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