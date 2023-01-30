using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MidtermTodos.Models;

public class Todo
{
    public int Id { get; set; }
    
    [Required]
    public IdentityUser Owner { get; set; }
    
    [Required, MinLength(1), MaxLength(200)]
    public string Task { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }
    
    public bool IsDone { get; set; }
}