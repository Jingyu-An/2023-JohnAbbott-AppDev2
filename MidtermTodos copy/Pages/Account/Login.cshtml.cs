using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MidtermTodos.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<RegisterModel> logger;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty] 
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required] 
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(Input.Username, Input.Password, false, true);
                if (result.Succeeded)
                {
                    // signInManager.SignInAsync(user, isPersistent: false);
                    logger.LogInformation($"User {Input.Username} logged in");
                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError(string.Empty, "Login failed" +
                                                       "(user does not exist, password invalid, " +
                                                       "account locked out)");
            }
            return Page();
        }
    }
}
