using UrlShortener.DataAccess.Models;

namespace UrlShortener.DataAccess.Repositories;

//I could use a generic repository, but my application only has one model, so using a generic repository would be an unnecessary complication.
public interface IShortenedUrlRepository
{
    IQueryable<ShortenedUrl> GetAllShortenedUrls();
    Task<ShortenedUrl> CreateShortenedUrlAsync(ShortenedUrl shortenedUrl);
    Task<ShortenedUrl> UpdateShortenedUrlAsync(ShortenedUrl shortenedUrlToUpdate);
    void RemoveShortenedUrl(uint id);

    Task<bool> SaveChangesAsync();
}
