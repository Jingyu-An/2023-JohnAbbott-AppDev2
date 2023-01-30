using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MidtermTodos.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<RegisterModel> logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger)
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
