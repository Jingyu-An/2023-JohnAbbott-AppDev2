using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages;

public class ArticleView : PageModel
{
    private ArticleContext db;

    public ArticleView(ArticleContext db) => this.db = db;
    
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    
    [BindProperty]
    public Article Article { get; set; }

    public void OnGet()
    {
        Article = db.Articles.Find(Id);
    }
}