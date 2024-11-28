using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.BussinessLogic.Services.Identity;

namespace UrlShortener.WebAPI.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(IIdentityService identityService)
        {
                
        }
        public void OnGet()
        {
        }
    }
}
