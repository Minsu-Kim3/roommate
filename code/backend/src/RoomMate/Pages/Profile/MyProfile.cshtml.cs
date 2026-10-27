// Retrieves the user's profile from the database and displays its details.
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;
namespace RoomMate.Pages.Profile;

public class MyProfileModel : PageModel
{
    private readonly RoomMateDbContext _db;

    public MyProfileModel(RoomMateDbContext db)
    {
        _db = db;
    }

    public RoomMate.Models.Profile? Profile { get; set; }

    public void OnGet()
    {
        // Temporary UserId until Login is implemented
        int userId = 1;

        Profile = RoomMate.Models.Profile.GetProfileByUserId(
            _db, userId);
    }
}