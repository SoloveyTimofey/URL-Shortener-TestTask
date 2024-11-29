using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.DataAccess.Context;
using UrlShortener.DataAccess.Repositories.ShortenedUrlRepository;

namespace UrlShortener.DataAccess;

public static class DataAccessServicesRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<UrlShortenerContext>(options => {
        //    options.UseSqlServer(configuration.GetConnectionString("UrlShortenerConnection"),
        //            b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
        //});

        //I use InMemory db for simplicity
        services.AddDbContext<UrlShortenerContext>(options => {
            options.UseInMemoryDatabase("UrlShortenerMemDb");
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
        });

        services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<UrlShortenerContext>();

        return services;
    }
}