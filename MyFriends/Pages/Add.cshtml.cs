using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using MyFriends.Data;
using MyFriends.Models;

namespace MyFriends.Pages;

public class Add : PageModel
{
    private FriendContext db;
    public Add(FriendContext db) => this.db = db;
    
    [BindProperty]
    public Friend NewFriend { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        await db.AddAsync(NewFriend);
        db.SaveChanges();
        if (ModelState.IsValid)
        {
            return RedirectToPage("index");
        }

        return Page();
    }
}