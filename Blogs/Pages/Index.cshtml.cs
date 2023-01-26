using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.Pages;

public class IndexModel : PageModel
{
    private readonly ArticleContext db;

    public IndexModel(ArticleContext db) => this.db = db;

    public List<Article> Articles { get; set; } = new List<Article>();

    public async Task OnGetAsync()
    {
        Articles = await db.Articles.ToListAsync();
    }
}