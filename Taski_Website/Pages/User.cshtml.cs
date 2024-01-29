using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Taski_Website.Model;
using System.Threading.Tasks;
using Taski_Website.Data;
using Microsoft.AspNetCore.Authorization;

namespace Taski_Website.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        private WebseiteContext context;
        public string UserId { get; private set; }
        public string UserEmail { get; private set; }
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public string UserImage{ get; private set; }
        public UserModel(WebseiteContext webseitenContext)
        {
            this.context = webseitenContext;
        }
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

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            UserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            UserName = User.FindFirst(ClaimTypes.Name)?.Value;
            if (UserId != null)
            {
                TaskiUser existingUser = await this.context.Users.FindAsync(int.Parse(this.UserId));
                if (existingUser != null)
                {
                    this.context.Users.Remove(existingUser);

                    var existingUserTasks = context.UserTask.Where(ut => ut.UserId == existingUser.UserId).ToList();
                    context.UserTask.RemoveRange(existingUserTasks);

                    await this.context.SaveChangesAsync();
                }
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
