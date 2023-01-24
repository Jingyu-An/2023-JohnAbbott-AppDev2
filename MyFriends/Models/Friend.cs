using System.ComponentModel.DataAnnotations;

namespace MyFriends.Models;

public class Friend
{
    public int Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; }
    [Range(1,150)]
    public int Age { get; set; }
}