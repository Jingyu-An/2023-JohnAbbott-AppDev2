using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MidtermTodos.Data;
using MidtermTodos.Models;

namespace MidtermTodos.Pages.Todos
{
    [Authorize]
    public class TodoDeleteModel : PageModel
    {
        private readonly TodoDbContext db;
        private ILogger<TodoDeleteModel> logger;

        public TodoDeleteModel(TodoDbContext db, ILogger<TodoDeleteModel> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        [BindProperty] public Todo Todo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || db.Todos == null)
            {
                return NotFound();
            }

            var todo = await db.Todos.FirstOrDefaultAsync(m => m.Id == id);

            if (todo == null)
            {
                return NotFound();
            }
            else
            {
                Todo = todo;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || db.Todos == null)
            {
                return NotFound();
            }

            var todo = await db.Todos.FindAsync(id);

            if (todo != null)
            {
                Todo = todo;
                db.Todos.Remove(Todo);
                await db.SaveChangesAsync();
            }

            logger.LogInformation($"User '{User.Identity.Name}' delete a todo task.");
            return RedirectToPage("./TodoDeleteSuccess");
        }
    }
}