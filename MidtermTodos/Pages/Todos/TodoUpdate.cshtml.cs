using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MidtermTodos.Data;
using MidtermTodos.Models;

namespace MidtermTodos.Pages.Todos
{
    [Authorize]
    public class TodoUpdateModel : PageModel
    {
        private readonly TodoDbContext db;
        private ILogger<TodoUpdateModel> logger;

        public TodoUpdateModel(TodoDbContext db, ILogger<TodoUpdateModel> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty] 
        public Todo Todo { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || db.Todos == null)
            {
                return NotFound();
            }

            var todo = await db.Todos.Include("Owner").FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            Todo = todo;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var todo = await db.Todos.FindAsync(Id);
            ModelState.Remove("Todo.Owner");
            if (ModelState.IsValid)
            {
                todo.Task = Todo.Task;
                todo.DueDate = Todo.DueDate;
                todo.IsDone = Todo.IsDone;
                
                if (todo.DueDate.Year < 2000 || todo.DueDate.Year > 2099)
                {
                    ModelState.AddModelError(string.Empty, "Due Date must be " +
                                                           "year in 2000-2099");
                    return Page();
                }

                db.Todos.Update(todo);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                logger.LogInformation($"User '{User.Identity.Name}' update a todo task.");
                return RedirectToPage("./TodoUpdateSuccess");
            }
            return Page();
        }

        private bool TodoExists(int id)
        {
            return (db.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}