using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.DataAccess.Models;

public class ShortenedUrl
{
    public uint Id { get; set; }
    public required string OriginalUrl { get; set; }
    public required string Shortened { get; set; }
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [ForeignKey(nameof(IdentityUser))]
    public required string CreatorId { get; set; }
    public IdentityUser? IdentityUser { get; set; }
}