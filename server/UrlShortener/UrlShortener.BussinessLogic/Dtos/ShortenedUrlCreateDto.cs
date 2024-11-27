using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.BussinessLogic.Dtos;

public record ShortenedUrlCreateDto
{
    public required string OriginalUrl { get; init; }
    public required string CreatorId { get; init; }
}