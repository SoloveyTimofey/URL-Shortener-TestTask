using UrlShortener.DataAccess.Context;
using UrlShortener.DataAccess.Models;
using UrlShortener.Infrastructure.Constants;

namespace UrlShortener.DataAccess.Repositories;

internal class ShortenedUrlRepository : IShortenedUrlRepository
{
    private readonly UrlShortenerContext _context;
    public ShortenedUrlRepository(UrlShortenerContext context)
    {
        _context = context;
    }

    public IQueryable<ShortenedUrl> GetAllShortenedUrls()
    {
        return _context.ShortenedUrls;
    }

    public async Task<ShortenedUrl> CreateShortenedUrlAsync(ShortenedUrl shortenedUrl)
    {
        await _context.AddAsync(shortenedUrl);

        return shortenedUrl;
    }

    public void RemoveShortenedUrl(uint id)
    {
        _context.Remove(id);
    }

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() >= 0;

    public async Task<ShortenedUrl> UpdateShortenedUrlAsync(ShortenedUrl shortenedUrlToUpdate)
    {
        var existingEntity = await _context.ShortenedUrls.FindAsync(shortenedUrlToUpdate.Id);

        if (existingEntity == null) throw new KeyNotFoundException(ExceptionMessages.ShortenedUrlNotFound(shortenedUrlToUpdate.Id));

        _context.Entry(existingEntity).CurrentValues.SetValues(shortenedUrlToUpdate);

        return existingEntity;
    }
}