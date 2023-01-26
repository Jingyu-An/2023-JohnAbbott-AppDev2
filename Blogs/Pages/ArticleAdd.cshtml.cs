using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages;

public class ArticleAdd : PageModel
{
    private ArticleContext db;

    public ArticleAdd(ArticleContext db) => this.db = db;
    
    [BindProperty]
    public Article NewArticle { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        NewArticle.Timestamp = DateTime.Now;
        db.Articles.Add(NewArticle);
        await db.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}