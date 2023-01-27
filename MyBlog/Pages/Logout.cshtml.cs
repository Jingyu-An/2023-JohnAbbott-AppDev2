using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyBlog.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<Register> logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager,
            ILogger<Register> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            if (signInManager.IsSignedIn(User))
            {
                logger.LogInformation($"User {User.Identity.Name} logged out");
            }
            await signInManager.SignOutAsync();

            return Page();
        }
    }
}
