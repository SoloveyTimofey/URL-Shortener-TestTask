using Microsoft.Extensions.DependencyInjection;
using UrlShortener.BussinessLogic.Services.Identity;
using UrlShortener.BussinessLogic.Services.ShortenUrl;

namespace UrlShortener.BussinessLogic;

public static class BusinessLogicServicesRegistration
{
    public static IServiceCollection AddBussinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IShortenUrlService, ShortenUrlService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}