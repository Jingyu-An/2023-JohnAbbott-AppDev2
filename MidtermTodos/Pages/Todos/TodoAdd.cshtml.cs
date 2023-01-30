using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MidtermTodos.Data;
using MidtermTodos.Models;

namespace MidtermTodos.Pages.Todos
{
    [Authorize]
    public class TodoAddModel : PageModel
    {
        private readonly TodoDbContext db;
        private ILogger<TodoAddModel> logger;

        public TodoAddModel(TodoDbContext db, ILogger<TodoAddModel> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        [BindProperty, Required, MinLength(1), MaxLength(200), Display(Name = "Task")]
        public string Task { get; set; }

        [BindProperty, Required, Display(Name = "DueDate")]
        public DateTime DueDate { get; set; }

        public DateTime Today = DateTime.Today;

        [BindProperty] public bool IsDone { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
                if (DueDate.Year < 2000 || DueDate.Year > 2099)
                {
                    ModelState.AddModelError(string.Empty, "Due Date must be " +
                                                           "year in 2000-2099");
                    return Page();
                }

                var newTodo = new Todo
                    { Task = Task, DueDate = DueDate, Owner = user, IsDone = IsDone };

                db.Add(newTodo);
                await db.SaveChangesAsync();
                
                logger.LogInformation($"User '{User.Identity.Name}' add a todo task.");
                return RedirectToPage("./TodoAddSuccess");
            }

            return Page();
        }
    }
}