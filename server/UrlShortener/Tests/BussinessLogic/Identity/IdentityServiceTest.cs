using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UrlShortener.BussinessLogic;
using UrlShortener.BussinessLogic.Services.Identity;
using UrlShortener.DataAccess.Models;
using UrlShortener.Infrastructure.Constants;

namespace Tests.BussinessLogic.Identity;

internal class IdentityServiceTest
{
    private IConfiguration _configuration;
    private UserManager<IdentityUser> _userManager;
    private IdentityService _identityService;
    [SetUp]
    public void SetUp()
    {
        SetConfigurationFake();
        SetUserManagerFake();
    }

    #region Register
    [Test]
    public async Task Register_ReceivesUserNameThatExists_ResultContainsValidErrorMessage()
    {
        //Assign
        string userName = "Joe";

        _userManager.FindByNameAsync(Arg.Any<string>()).Returns(new IdentityUser
        {
            UserName = userName,
        });

        //Act
        var result = await _identityService.RegisterAsync(userName, "testPassword", "testEmail@test.com");

        //Assert
        Assert.Contains(ExceptionMessages.UserNameIsAlreadyTaken, result.Errors);
    }

    [Test]
    public async Task Register_CreateUserUnsucceed_ReturnResultUnsucceed()
    {
        //Assign
        string errorDescription = "User name already exists.";

        _userManager.CreateAsync(Arg.Any<IdentityUser>(), Arg.Any<string>())
               .Returns(Task.FromResult(IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "DuplicateUserName",
                        Description = errorDescription
                    }
               )));

        //Act
        var result = await _identityService.RegisterAsync("testUser", "testPassword", "testemail@test.com");

        //Assert
        Assert.Contains(errorDescription, result.Errors);
    }

    [Test]
    public void Register_GenerateTokenProvidedWithIncorrectTypeOfExpirationTime_ThrowsValidException()
    {
        //Assign
        _configuration["JwtConfiguration:MinutesAfterKeyExpires"].Returns("NotInt");

        //Act & Assert
        Assert.ThrowsAsync<ApplicationException>(
             () => _identityService.RegisterAsync("testUser", "testPasswrod", "testemail@test.com"),
            ExceptionMessages.ExpirationTimeIsNotInt
        );
    }

    [Test]
    public void Register_SecretAndIssuerAndAudienceNotProvided_ThrowsValidException()
    {
        //Assign
        _configuration["JwtConfiguration:Secret"] = null;
        _configuration["JwtConfiguration:ValidIssuer"] = null;
        _configuration["JwtConfiguration:ValidAudiences"] = null;

        //Act & Assert
        Assert.ThrowsAsync<ApplicationException>(
            () => _identityService.RegisterAsync("testUser", "testPassword", "testemail@test.com"),
            ExceptionMessages.JwtIsNotSetInTheConfiguration);
    }

    [Test]
    public async Task Register_RegistrationSuccessfull_ReturnsToken()
    {
        //Assign & Act
        var result = await _identityService.RegisterAsync("testName", "testPassword", "testemail@test.com");

        //Assert
        Assert.IsFalse(string.IsNullOrEmpty(result.Token));
    }
    #endregion

    #region Login
    [Test]
    public async Task Login_FindByNameReturnsNull_ResultContainsValidErrorMessage()
    {
        //Act
        var result = await _identityService.LoginAsync("testUser", "testPassword");

        //Assert
        Assert.Contains(ExceptionMessages.InvalidUserNameOrPassword, result.Errors);
    }

    [Test]
    public async Task Login_CheckPasswordAsyncReturnFalse_ResultContainsValidErrorMessage()
    {
        //Act
        var result = await _identityService.LoginAsync("testUser", "testPassword");

        //Assert
        Assert.Contains(ExceptionMessages.InvalidUserNameOrPassword, result.Errors);
    }

    [Test]
    public async Task Login_Successful_ReturnsToken()
    {
        //Assign
        _userManager.CheckPasswordAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(true);
        _userManager.FindByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<IdentityUser?>(new IdentityUser { }));

        //Act
        var result = await _identityService.LoginAsync("testUser", "testPassword");

        //Assert
        Assert.NotNull(result.Token);
    }
    #endregion

    #region DeleteAccount
    [Test]
    public async Task DeleteAccount_FindByNameReturnsNull_ResultContainsValidErrorMessage()
    {
        //Act
        var result = await _identityService.DeleteAccountAsync("testUser", "testPassword");

        //Assert
        Assert.Contains(ExceptionMessages.InvalidUserNameOrPassword, result.Errors);
    }

    [Test]
    public async Task DeleteAccount_CheckPasswordReturnFalse_ResultContainsValidErrorMessage()
    {
        //Assert
        _userManager.FindByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<IdentityUser?>(new IdentityUser { }));

        //Act
        var result = await _identityService.DeleteAccountAsync("testUser", "testPassword");

        //Assert
        Assert.Contains(ExceptionMessages.InvalidUserNameOrPassword, result.Errors);
    }

    [Test]
    public async Task DeleteAccount_DeleteAsyncUnsucceed_ResultContainsValidErrorMessage()
    {
        //Assign
        _userManager.FindByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<IdentityUser?>(new IdentityUser { }));
        _userManager.CheckPasswordAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(true);

        string errorDescription = "Delete error";
        _userManager.DeleteAsync(Arg.Any<IdentityUser>())
            .Returns(IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "DuplicateUserName",
                        Description = errorDescription
                    })
            );

        //Act
        var result = await _identityService.DeleteAccountAsync("testUser", "testPassword");

        //Assert
        Assert.Contains(errorDescription, result.Errors);
    }

    //[Test]
    //public async Task DeleteAccount_Successful_ResultSuccessful()
    //{
    //    //Assign
    //    _userManager.FindByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<ApplicationUser?>(new ApplicationUser { }));
    //    _userManager.CheckPasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(true);
    //    _userManager.DeleteAsync(Arg.Any<ApplicationUser>()).Returns(Task.FromResult(IdentityResult.Success));

    //    //Act
    //    var result = await _identityService.DeleteAccountAsync("testUser", "testPassword");

    //    //Assert
    //    Assert.True(result.Succeeded);
    //}
    #endregion

    private void SetUserManagerFake()
    {
        var store = Substitute.For<IUserStore<IdentityUser>>();
        _userManager = Substitute.For<UserManager<IdentityUser>>(
            store,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );
        _userManager.CreateAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(Task.FromResult(IdentityResult.Success));
        _userManager.FindByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<IdentityUser?>(null));
        _userManager.AddToRoleAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(Task.FromResult(IdentityResult.Success));
        _userManager.CheckPasswordAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(false);

        _identityService = new IdentityService(_userManager, _configuration);
    }

    private void SetConfigurationFake()
    {
        _configuration = Substitute.For<IConfiguration>();

        _configuration["JwtConfiguration:Secret"].Returns(Guid.NewGuid().ToString());
        _configuration["JwtConfiguration:ValidIssuer"].Returns("https://localhost:7091");
        _configuration["JwtConfiguration:ValidAudiences"].Returns("https://localhost:4200");
        _configuration["JwtConfiguration:MinutesAfterKeyExpires"].Returns("50");
    }
}