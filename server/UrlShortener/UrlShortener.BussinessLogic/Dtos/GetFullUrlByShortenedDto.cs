namespace UrlShortener.BussinessLogic.Dtos;

public record GetFullUrlByShortenedDto
{
    public required string ShortenedUrl { get; init; }
}