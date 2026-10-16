using System.ComponentModel.DataAnnotations;

namespace RoomMate.Models;

// Temporary User model until the Account feature is ready
public class TempUser
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    // One user can have one profile
    public Profile? Profile { get; set; }
}
