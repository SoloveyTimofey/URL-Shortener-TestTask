using System.Text;
using System.Security.Cryptography;


namespace UrlShortener.BussinessLogic.Utils;

public static class ShortenedUrlGenerator
{
    public static string ShortenUrl(string originalUrl)
    {
        if (string.IsNullOrWhiteSpace(originalUrl))
        {
            throw new ArgumentException("Original URL cannot be null or empty.", nameof(originalUrl));
        }

        // Generating a hash from the original URL
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalUrl));

            // Convert first bytes of hash to Base64 and trim
            var base64Hash = Convert.ToBase64String(hashBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');

            // Return first 8 characters as shortened URL
            return base64Hash.Substring(0, 8);
        }
    }
}
