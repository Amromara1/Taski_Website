using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Taski_Website.Data;
using Taski_Website.Model;

namespace Taski_Website.Pages
{
    public class LoginModel : PageModel
    {
       
        private WebseiteContext context;
        [BindProperty]
        public int UserId { get; set; }

        [BindProperty, Required, Display(Name = "Please add an Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        public LoginModel( WebseiteContext webseitenContext)
        {
            
            this.context = webseitenContext;
        }
        public List<TaskiUser> Users { get; set; } = new();
        public async Task OnGetAsync()
        {

            Users = await context.Users.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TaskiUser existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (existingUser != null)
            {
                bool CorrectPass = Encryption.VerifyPassword(this.Password, existingUser.Password);
                if (!CorrectPass)
                {
                    ModelState.AddModelError(nameof(Password), "WORNG PASSWORD ¯|_(?)_|¯.");
                    return Page();
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", existingUser.UserId);
                    HttpContext.Session.SetString("UserEmail", existingUser.Email);
                    return RedirectToPage("Task");
                }
                
            }else
            {
                ModelState.AddModelError(nameof(Email), "E-mail does not exist.");
                return Page();
            }
            
        }

    }
}
