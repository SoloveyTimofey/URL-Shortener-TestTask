using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.BussinessLogic.Services.Identity;
using UrlShortener.DataAccess.Context;
using UrlShortener.Infrastructure.Constants;
using UrlShortener.DataAccess.Models;
using UrlShortener.BussinessLogic.Utils;
using UrlShortener.BussinessLogic.Services.ShortenUrl;
using UrlShortener.BussinessLogic.Dtos;
using UrlShortener.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.DataAccess.DbPopulator;
public static class DatabasePopulator
{
    public async static void PopulateDbAsync(IApplicationBuilder app)
    {
        using(var serviceScope = app.ApplicationServices.CreateScope())
        {
            await SeedIdentityData(serviceScope);
            await SeedUrlData(serviceScope);

            await serviceScope.ServiceProvider.GetRequiredService<UrlShortenerContext>().SaveChangesAsync();
        }
    }

    private async static Task SeedIdentityData(IServiceScope serviceScope)
    {
        var services = serviceScope.ServiceProvider;

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = IdentityRoles.GetAllRoles();
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var identityService = services.GetRequiredService<IIdentityService>();

        await identityService.RegisterAsync("Administrator", "testUser", "admin@gmail.com", true);
        await identityService.RegisterAsync("UserFirst", "testUser", "user1@gmail.com", true);
        await identityService.RegisterAsync("UserSecond", "testUser", "user2@gmail.com", true);
    }

    private async static Task SeedUrlData(IServiceScope serviceScope)
    {
        var services = serviceScope.ServiceProvider;

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        var admin = await userManager.FindByEmailAsync("admin@gmail.com");
        var user1 = await userManager.FindByEmailAsync("user1@gmail.com");
        var user2 = await userManager.FindByEmailAsync("user2@gmail.com");

        var shortenUrlService = services.GetRequiredService<IShortenUrlService>();

        var tasks = new List<Task>
        {
            shortenUrlService.CreateShortenedUrlAsync(GetShortenedUrlCreateDto(admin!.Id, "https://music.youtube.com/watch?v=2XqpXmhM1Fc&list=RDAMVM2XqpXmhM1Fc")),
            shortenUrlService.CreateShortenedUrlAsync(GetShortenedUrlCreateDto(user1!.Id, "https://online-voice-recorder.com/ru/")),
            shortenUrlService.CreateShortenedUrlAsync(GetShortenedUrlCreateDto(user2!.Id, "https://www.google.com/search?q=%D0%BF%D0%B5%D1%80%D0%B5%D0%B2%D0%BE%D0%B4%D1%87%D0%B8%D0%BA&rlz=1C1SQJL_ruUA819UA819&oq=%D0%BF%D0%B5%D1%80%D0%B5%D0%B2%D0%BE%D0%B4%D1%87%D0%B8%D0%BA&aqs=chrome..69i57j0l5.1951j0j7&sourceid=chrome&ie=UTF-8")),
            shortenUrlService.CreateShortenedUrlAsync(GetShortenedUrlCreateDto(user1!.Id, "https://meteofor.com.ua/")),
            shortenUrlService.CreateShortenedUrlAsync(GetShortenedUrlCreateDto(user2!.Id, "https://discord.com/channels/@me"))
        };

        await Task.WhenAll(tasks);
    }

    private static ShortenedUrlCreateDto GetShortenedUrlCreateDto(string creatorId, string originalUrl)
    {
        return new ShortenedUrlCreateDto
        {
            CreatorId = creatorId,
            OriginalUrl = originalUrl
        };
    }
}
