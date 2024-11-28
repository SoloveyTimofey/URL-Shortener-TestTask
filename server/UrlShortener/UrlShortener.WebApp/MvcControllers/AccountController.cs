using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UrlShortener.Infrastructure.Constants;
using UrlShortener.WebApp.ViewModels.Account;

namespace UrlShortener.WebApp.MvcControllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var identityUser = await _userManager.FindByEmailAsync(viewModel.Email);

            if (identityUser == null)
            {
                ModelState.AddModelError("", ExceptionMessages.InvalidUserNameOrPassword);
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(identityUser, viewModel.Password, true, false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel viewModel)
    {
        if(ModelState.IsValid)
        {
            IdentityUser identityUser = new IdentityUser
            {
                Email = viewModel.Email,
                UserName = viewModel.UserName
            };

            var passwordResult = await _userManager.CreateAsync(identityUser, viewModel.Password);

            if (passwordResult.Succeeded is false)
            {
                foreach (var error in passwordResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View();
            }

            IdentityResult? identityResult = await _userManager.AddToRoleAsync(identityUser, IdentityRoles.User);

            if (identityResult.Succeeded is false)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View();
            }

            return RedirectToAction("Login", "Account");
        }

        return View();
    }

    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

}