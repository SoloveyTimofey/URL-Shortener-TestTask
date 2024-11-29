namespace UrlShortener.BussinessLogic.Dtos;

public record ShortenedUrlReadDto
{
    public uint Id { get; init; }
    public required string OriginalUrl { get; init; }
    public required string Shortened { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatorId { get; init; }
    public required string CreatorName { get; set; }
}