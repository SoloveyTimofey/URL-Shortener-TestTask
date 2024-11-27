using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.BussinessLogic.Services.Identity;
using UrlShortener.DataAccess.Context;
using UrlShortener.Infrastructure.Constants;

namespace UrlShortener.DataAccess.DbPopulator;
public static class DatabasePopulator
{
    public async static void PopulateDbAsync(IApplicationBuilder app)
    {
        using(var serviceScope = app.ApplicationServices.CreateScope())
        {
            await SeedIdentityData(serviceScope);
            await SeedUrlData(serviceScope);
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

        UrlShortenerContext context = services.GetRequiredService<UrlShortenerContext>();

        //context.ShortenedUrls.AddRangeAsync(
        //    new Models.ShortenedUrl { CreatorId = admin.Id, OriginalUrl = "https://music.youtube.com/" })
    }
}
