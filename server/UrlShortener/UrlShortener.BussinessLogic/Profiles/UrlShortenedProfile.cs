using AutoMapper;
using UrlShortener.BussinessLogic.Dtos;
using UrlShortener.DataAccess.Models;

namespace UrlShortener.BussinessLogic.Profiles;

public class UrlShortenedProfile : Profile
{
    public UrlShortenedProfile()
    {
        CreateMap<ShortenedUrl, ShortenedUrlReadDto>();
        CreateMap<ShortenedUrl, ShortenedUrlUpdateDto>();
        CreateMap<ShortenedUrl, ChangeOriginalUrlDto>();
        CreateMap<ShortenedUrl, ShortenedUrlReadForUnauthorizedUsersDto>();
        CreateMap<ShortenedUrlCreateDto, ShortenedUrl>();
    }
}