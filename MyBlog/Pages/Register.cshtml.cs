using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyBlog.Pages;

public class Register : PageModel
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly ILogger<Register> logger;

    [BindProperty] 
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }
    
    public Register(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager ,
        ILogger<Register> logger)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.logger = logger;
    }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "the {0} must be at least {2} and max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                // signInManager.SignInAsync(user, isPersistent: false);
                logger.LogInformation($"User {Input.Email} create a new account with password.");
                return RedirectToPage("RegisterSuccess", new { email = Input.Email });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return Page();
    }
}