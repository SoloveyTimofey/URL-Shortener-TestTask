using AutoMapper;
using UrlShortener.BussinessLogic.Services.ShortenUrl;
using UrlShortener.DataAccess.Models;
using UrlShortener.DataAccess.Repositories.ShortenedUrlRepository;

namespace Tests.BussinessLogic.Services.ShortenUrl;

//This test is not competed
internal class ShortenUrlServiceTests
{
    private IShortenedUrlRepository _shortenedUrlRepository;
    private List<ShortenedUrl> _shortenedUrlsTestObjects;
    private ShortenUrlService _shortenUrlService;
    [SetUp]
    public void SetUp()
    {
        _shortenedUrlsTestObjects = new List<ShortenedUrl>
        {
            new ShortenedUrl
            {
                OriginalUrl = "someOriginalUrl",
                CreatorId = "id",
                Shortened = "shortened"
            },
            new ShortenedUrl
            {
                OriginalUrl = "someOriginalUrl2",
                CreatorId = "id2",
                Shortened = "shortened2"
            }
        };

        SetShortenedUrlRepositoryFake();
        SetShortenUrlServiceFake();
    }


    //[Test]
    //public async Task GetFullUrlByShortenedAsync_ReturnsValidObject()
    //{
    //    //Arrange
    //    var urlToCompare = _shortenedUrlsTestObjects.First();

    //    var getFullUrlByShortenedDto = new GetFullUrlByShortenedDto
    //    {
    //        ShortenedUrl = urlToCompare.Shortened
    //    };

    //    //Act

    //    //System.InvalidOperationException : The provider for the source 'IQueryable' doesn't implement 'IAsyncQueryProvider'. Only providers that implement 'IAsyncQueryProvider' can be used for Entity Framework asynchronous operations.
    //    var result = await _shortenUrlService.GetFullUrlByShortenedAsync(getFullUrlByShortenedDto);

    //    //Assert
    //    Assert.True(result.OriginalUrl == urlToCompare.OriginalUrl);
    //}

    private void SetShortenedUrlRepositoryFake()
    {
        _shortenedUrlRepository = Substitute.For<IShortenedUrlRepository>();

        _shortenedUrlRepository.GetAllShortenedUrls().Returns(_shortenedUrlsTestObjects.AsQueryable());
    }

    private void SetShortenUrlServiceFake()
    {
        IMapper mapper = Substitute.For<IMapper>();

        _shortenUrlService = new ShortenUrlService(_shortenedUrlRepository, mapper);
    }
}