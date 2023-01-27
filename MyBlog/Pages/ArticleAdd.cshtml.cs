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
    public ArticleAdd(BlogDbContext db, ILogger<ArticleAdd> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    [BindProperty, Required, MinLength(1), MaxLength(200), Display(Name = "Title")]
    public string Title { get; set; }
    
    [BindProperty, Required, MinLength(1), MaxLength(2000), Display(Name = "Content")]
    public string Body { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var newArticle = new Article { Title = Title, Body = Body, Author = user, Created = DateTime.Now};
            
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