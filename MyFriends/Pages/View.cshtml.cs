using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using MyFriends.Data;
using MyFriends.Models;

namespace MyFriends.Pages;

public class View : PageModel
{
    private FriendContext db;
    public View(FriendContext db) => this.db = db;
    
    [BindProperty(SupportsGet =true)]
    public int Id { get; set; }
    public Friend Friend { get; set; }

    public async Task OnGetAsync() => Friend = await db.Friends.FindAsync(Id);
}