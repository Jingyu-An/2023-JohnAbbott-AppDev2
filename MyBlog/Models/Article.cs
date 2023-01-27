using System;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models;

public class Article
{
    public int Id { get; set; }
    public IdentityUser Author { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime Created { get; set; }
}