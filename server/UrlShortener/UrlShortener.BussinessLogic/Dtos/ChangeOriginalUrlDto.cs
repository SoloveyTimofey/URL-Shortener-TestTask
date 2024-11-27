namespace UrlShortener.BussinessLogic.Dtos;

public record ChangeOriginalUrlDto
{
    public uint Id { get; init; }
    public required string OriginalUrl { get; init; }
}
