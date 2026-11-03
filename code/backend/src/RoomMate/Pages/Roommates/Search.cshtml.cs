//shows other students ranked by compatibility.
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;
using RoomMate.Models;

namespace RoomMate.Pages.Roommates;

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

    public void OnGet()
    {
        //temp UserId wait until Login is implemented
        int userId = 1;

        //keep the filter in range instead of error
        MinScore = Math.Clamp(MinScore, 0, 100);

        var myProfile = RoomMate.Models.Profile.GetProfileByUserId(_db, userId);

        HasProfile = myProfile != null;
        ProfileComplete = myProfile != null && myProfile.IsComplete();

        if (!ProfileComplete){
            return;
        }

        Results = RoomMate.Models.Profile.SearchRoommates( _db, userId, MinScore);
    }
}
