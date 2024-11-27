using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortener.DataAccess.Models;

namespace UrlShortener.DataAccess.Context;

internal class UrlShortenerContext : IdentityDbContext<IdentityUser>
{
    public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options) { }

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
}