using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UrlShortener.DataAccess.Context;
using UrlShortener.DataAccess.Models;
using UrlShortener.DataAccess.Repositories;

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

        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UrlShortenerContext>();
            

        return services;
    }
}