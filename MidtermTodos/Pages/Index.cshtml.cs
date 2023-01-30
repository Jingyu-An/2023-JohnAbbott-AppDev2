using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MidtermTodos.Models;

public class IndexModel : PageModel
{
    private readonly MidtermTodos.Data.TodoDbContext _context;

    public IndexModel(MidtermTodos.Data.TodoDbContext context)
    {
        _context = context;
    }

    public IList<Todo> Todo { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Todos != null)
        {
            Todo = await _context.Todos.Include("Owner").ToListAsync();
        }
    }
}