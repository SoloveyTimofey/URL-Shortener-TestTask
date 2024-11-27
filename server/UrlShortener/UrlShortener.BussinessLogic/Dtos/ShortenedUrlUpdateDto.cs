namespace UrlShortener.BussinessLogic.Dtos;

public record ShortenedUrlUpdateDto
{
    public required string OriginalUrl { get; init; }
    public required string Shortened { get; init; }
    public required string CreatorId { get; init; }
}