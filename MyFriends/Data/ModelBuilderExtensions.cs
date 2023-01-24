using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.Data;

public static class ModelBuilderExtensions
{
    public static ModelBuilder Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>().HasData(
            new Friend
            {
                Id = 1,
                Name = "Smith",
                Age = 20
            },
            new Friend
            {
                Id = 2,
                Name = "John",
                Age = 21
            }
        );
        return modelBuilder;
    }
}