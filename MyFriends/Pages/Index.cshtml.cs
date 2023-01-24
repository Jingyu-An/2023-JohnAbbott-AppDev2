using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyFriends.Data;
using MyFriends.Models;

namespace MyFriends.Pages;

public class IndexModel : PageModel
{
    private readonly FriendContext db;
    public IndexModel(FriendContext db) => this.db = db;
    public List<Friend> Friends { get; set; } = new List<Friend>();

    public async Task OnGetAsync()
    {
        Friends = await db.Friends.ToListAsync();
    }
}