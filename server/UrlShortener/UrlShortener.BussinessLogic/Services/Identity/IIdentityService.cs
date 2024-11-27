using UrlShortener.BussinessLogic.Models;

namespace UrlShortener.BussinessLogic.Services.Identity;

public interface IIdentityService
{
    Task<ApplicationIdentityResultWithReturnValue> RegisterAsync(string userName, string password, string email, bool registerAdmin = false);
    Task<ApplicationIdentityResult> DeleteAccountAsync(string userId, string password);
    Task<ApplicationIdentityResultWithReturnValue> LoginAsync(string userName, string password);
}