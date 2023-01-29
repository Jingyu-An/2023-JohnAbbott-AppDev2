using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Pages;

public class IndexModel : PageModel
{
    private readonly BlogDbContext _context;

    public IndexModel(BlogDbContext context)
    {
        _context = context;
    }

    public IList<Article> Article { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Articles != null)
        {
            Article = await _context.Articles.Include("Author").ToListAsync();
        }
    }
}