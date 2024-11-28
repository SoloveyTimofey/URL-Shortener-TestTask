using UrlShortener.BussinessLogic.Dtos;
using UrlShortener.DataAccess.Models;

namespace UrlShortener.BussinessLogic.Services.ShortenUrl;

public interface IShortenUrlService
{
    Task<ICollection<ShortenedUrlReadDto>> GetAllShortenedUrlsAsync();
    Task<ICollection<ShortenedUrlReadForUnauthorizedUsersDto>> GetAllShortenedUrlsForUnathorizedUsersAsync();
    Task<ShortenedUrl> GetFullUrlByShortenedAsync(GetFullUrlByShortenedDto getFullUrlByShortenedDto);
    Task<ShortenedUrl> CreateShortenedUrlAsync(ShortenedUrlCreateDto shortenedUrlCreateDto);
    Task<ShortenedUrl> ChangeOriginalUrlAsync(ChangeOriginalUrlDto chnageOriginalUrlDto, string executorId);
    Task RemoveShortenedUrlAsync(uint id, string executorId);
}