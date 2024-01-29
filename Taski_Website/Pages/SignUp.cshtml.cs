using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Taski_Website.Data;
using Taski_Website.Model;

namespace Taski_Website.Pages
{
    public class SignUpModel : PageModel
    {

        private WebseiteContext context;

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty, Required, Display(Name = "Please add an Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [BindProperty, Required]
        [RegularExpression(@"^(?=.*[@$!%*#?&]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one special character.")]
        public string Password { get; set; }


        [BindProperty, Required]        
        public string Role { get; set; }

        [BindProperty, Required, Display(Name = "Please add a Username")]
        public string UserName { get; set; }

        public SignUpModel(WebseiteContext webseitenContext)
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

            var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(nameof(Email), "Email already exists.");
                return Page();
            }

            var newUser = new TaskiUser
            {
                UserId = this.context.Users.Max(x => x.UserId) + 1,
                Email = Email,
                Password = Encryption.EncryptPassword(Password),
                Role = Role,
                UserName = UserName
            };

            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();

            return RedirectToPage("Login");
        }

    }
}
