namespace UrlShortener.BussinessLogic.Dtos;

public record ShortenedUrlReadForUnauthorizedUsersDto
{
    public uint Id { get; init; }
    public required string OriginalUrl { get; init; }
    public required string Shortened { get; init; }
}