using UrlShortener.BussinessLogic.Utils;
using UrlShortener.Infrastructure.Constants;

namespace Tests.BussinessLogic.Utils;

internal class ShortenedUrlGeneratorTests
{
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void ShortenUrl_NullOrWhiteSpaceUrl_ThrowsValidException(string originalUrl)
    {
        //Assert & Assert
        Assert.Throws<ArgumentException>(
            () => ShortenedUrlGenerator.ShortenUrl(originalUrl),
           ExceptionMessages.OriginalURLCannotBeNullOrEmpty
        );
    }

    [Test]
    public void ShortenUrl_ValidUrl_ReturnsShortenedUrl()
    {
        // Arrange
        string originalUrl = "https://example.com";

        // Act
        string shortenedUrl = ShortenedUrlGenerator.ShortenUrl(originalUrl);

        // Assert
        Assert.IsNotNull(shortenedUrl);
        Assert.That(shortenedUrl.Length == 8);
    }

    [Test]
    public void ShortenUrl_DifferentUrls_ProduceDifferentShortenedUrls()
    {
        // Arrange
        string url1 = "https://example1.com";
        string url2 = "https://example2.com";

        // Act
        string shortenedUrl1 = ShortenedUrlGenerator.ShortenUrl(url1);
        string shortenedUrl2 = ShortenedUrlGenerator.ShortenUrl(url2);

        // Assert
        Assert.That(shortenedUrl1 != shortenedUrl2);
    }

    [Test]
    public void ShortenUrl_SameUrl_ProducesSameShortenedUrl()
    {
        // Arrange
        string url = "https://example.com";

        // Act
        string shortenedUrl1 = ShortenedUrlGenerator.ShortenUrl(url);
        string shortenedUrl2 = ShortenedUrlGenerator.ShortenUrl(url);

        // Assert
        Assert.That(shortenedUrl1 == shortenedUrl2);
    }
}