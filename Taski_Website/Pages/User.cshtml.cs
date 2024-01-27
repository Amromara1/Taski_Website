using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Taski_Website.Model;

namespace Taski_Website.Pages
{
    public class UserModel : PageModel
    {
        public string UserId { get; private set; }
        public string UserEmail { get; private set; }
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public string UserImage{ get; private set; }
        public async Task OnGetAsync()
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            UserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            UserName = User.FindFirst(ClaimTypes.Name)?.Value;
            UserImage = await RandomUserImage.GetRandomUserPictureAsync();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the home page or any desired page after logout
            return RedirectToPage("/Index");
        }
    }
}
