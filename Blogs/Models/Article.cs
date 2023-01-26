using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models;

public class Article
{
    [Key] 
    public int Id { get; set; }

    public IdentityUser Author { get; set; }

    public string Title { get; set; }
    
    public string Body { get; set; }

    public DateTime Timestamp { get; set; }
}