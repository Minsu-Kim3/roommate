// Retrieves the user's profile from the database and displays its details.
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;
namespace RoomMate.Pages.Profile;

[Authorize]
public class MyProfileModel : PageModel
{
    private readonly RoomMateDbContext _db;

    public MyProfileModel(RoomMateDbContext db)
    {
        _db = db;
    }

    public RoomMate.Models.Profile? Profile { get; set; }

    public IActionResult OnGet()
    {
        // Get logged-in user's ID
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userId))
        {
            return Challenge();
        }

        // Get user's profile
        Profile = RoomMate.Models.Profile.GetProfileByUserId(_db, userId);

        if (Profile == null)
        {
            return RedirectToPage("/Profile/Create");
        }

        return Page();
    }
}