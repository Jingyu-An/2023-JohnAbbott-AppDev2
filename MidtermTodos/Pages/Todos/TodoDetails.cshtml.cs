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
    public class TodoDetailsModel : PageModel
    {
        private readonly TodoDbContext db;

        public TodoDetailsModel(TodoDbContext db)
        {
            this.db = db;
        }

      public Todo Todo { get; set; } = default!; 

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
            else 
            {
                Todo = todo;
            }
            return Page();
        }
    }
}
