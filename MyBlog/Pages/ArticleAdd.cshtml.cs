using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Pages;

[Authorize]
public class ArticleAdd : PageModel
{
    private BlogDbContext db;
    private ILogger<ArticleAdd> logger;
    private IWebHostEnvironment environment;

    public ArticleAdd(BlogDbContext db, ILogger<ArticleAdd> logger, IWebHostEnvironment environment)
    {
        this.db = db;
        this.logger = logger;
        this.environment = environment;
    }

    [BindProperty, Required, MinLength(1), MaxLength(200), Display(Name = "Title")]
    public string Title { get; set; }

    [BindProperty, Required, MinLength(1), MaxLength(2000), Display(Name = "Content")]
    public string Body { get; set; }

    [BindProperty] public IFormFile Upload { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            string imagePath = null;
            if (Upload != null)
            {
                string fileExtension = Path.GetExtension(Upload.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".gif", ".png" };

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError(string.Empty, "Only Image files" +
                                                           "(jpg, jpeg, gif, png) are allowed");
                    return Page();
                }

                var invalids = Path.GetInvalidPathChars();
                var newFileName = String.Join("_", Upload.FileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries))
                                            .TrimEnd('.');

                var destPathfile = Path.Combine(environment.ContentRootPath, "wwwroot", "Uploads", Upload.FileName);
                try
                {
                    using (var fileStream = new FileStream(destPathfile, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }
                }
                catch (Exception ex) when (ex is IOException || ex is SystemException)
                {
                    ModelState.AddModelError(string.Empty, "Internal error " +
                                                           "saving the uploaded file");
                    return Page();
                }

                imagePath = Path.Combine("Uploads", newFileName);
            }

            var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var newArticle = new Article
                { Title = Title, ImagePath = imagePath, Body = Body, Author = user, Created = DateTime.Now };

            db.Add(newArticle);
            await db.SaveChangesAsync();

            return RedirectToPage("ArticleAddSuccess");
        }

        return Page();
    }

    public void OnGet()
    {
    }
}