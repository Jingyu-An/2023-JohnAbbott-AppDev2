using Microsoft.EntityFrameworkCore;
using MyFriends.Data.Configurations;
using MyFriends.Models;

namespace MyFriends.Data;

public class FriendContext : DbContext
{
    public DbSet<Friend> Friends { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source=Friends.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FriendConfiguration());
    }
}