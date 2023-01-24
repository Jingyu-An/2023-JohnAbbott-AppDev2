using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.Data;

public class FriendContext : DbContext
{
    public DbSet<Friend> Friends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Date source=Friends.db");
    }
}