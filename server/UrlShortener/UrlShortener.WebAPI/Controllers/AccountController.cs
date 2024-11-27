using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.BussinessLogic.Services.Identity;
using URLShortener.WebAPI.Models;

namespace UrlShortener.WebAPI.Controllers;

[ApiController, Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IIdentityService _identityService;
    public AccountController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost, Route(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] AddOrUpdateApplicationUserModel userModel)
    {
        if (ModelState.IsValid)
        {
            var identityResult = await _identityService.RegisterAsync(userModel.Username, userModel.Password, userModel.Email);

            if (identityResult.Succeeded)
            {
                return Ok(identityResult.Token);
            }

            return BadRequest(identityResult.Errors);
        }

        return BadRequest(ModelState);
    }

    [HttpPost, Route(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var identityResult = await _identityService.LoginAsync(loginModel.UserName, loginModel.Password);

        if (identityResult.Succeeded)
        {
            return Ok(identityResult.Token);
        }

        return BadRequest(identityResult.Errors);
    }

    [Authorize]
    [HttpDelete, Route(nameof(DeleteAccount))]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountModel deleteAccountModel)
    {
        var identityResult = await _identityService.DeleteAccountAsync(deleteAccountModel.Username, deleteAccountModel.Password);

        if (identityResult.Succeeded)
        {
            return Ok();
        }

        return BadRequest(identityResult.Errors);
    }
}