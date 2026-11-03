//shows other students ranked by compatibility.
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;
using RoomMate.Models;

namespace RoomMate.Pages.Roommates;

//only logged-in users can open this page
[Authorize]
public class SearchModel : PageModel{
    private readonly RoomMateDbContext _db;

    public SearchModel(RoomMateDbContext db)
    {
        _db = db;
    }

    //min compatibility filter read from the URL (?MinScore=70)
    [BindProperty(SupportsGet = true)]
    public double MinScore { get; set; } = 0;

    public bool HasProfile { get; set; }

    public bool ProfileComplete { get; set; }

    public List<RoommateMatch> Results { get; set; } = new();

    public IActionResult OnGet()
    {
        //get the logged-in user's ID from the login cookie
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userId)){
            return Challenge();
        }

        //keep the filter in range instead of error
        MinScore = Math.Clamp(MinScore, 0, 100);

        var myProfile = RoomMate.Models.Profile.GetProfileByUserId(_db, userId);

        HasProfile = myProfile != null;
        ProfileComplete = myProfile != null && myProfile.IsComplete();

        if (!ProfileComplete){
            return Page();
        }

        Results = RoomMate.Models.Profile.SearchRoommates( _db, userId, MinScore);
        return Page();
    }
}