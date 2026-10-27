// Loads and updates the user's profile in the database.
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;

namespace RoomMate.Pages.Profile;

public class EditModel : PageModel
{
    private readonly RoomMateDbContext _db;

    public EditModel(RoomMateDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    [Range(1, 5)]
    public int SleepSchedule { get; set; }

    [BindProperty]
    [Range(1, 5)]
    public int Cleanliness { get; set; }

    [BindProperty]
    [Range(1, 5)]
    public int NoiseTolerance { get; set; }

    [BindProperty]
    [Required]
    public bool? IsSmoker { get; set; }

    [BindProperty]
    public bool OkWithSmoker { get; set; }

    public IActionResult OnGet()
    {
        // Temporary user until Login is implemented
        int userId = 1;

        var profile = RoomMate.Models.Profile.GetProfileByUserId(
            _db, userId);

        if (profile == null)
        {
            return NotFound();
        }

        // Load existing profile values
        SleepSchedule = profile.SleepSchedule;
        Cleanliness = profile.Cleanliness;
        NoiseTolerance = profile.NoiseTolerance;
        IsSmoker = profile.IsSmoker;
        OkWithSmoker = profile.OkWithSmoker;

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Temporary user until Login is implemented
        int userId = 1;

        var profile = RoomMate.Models.Profile.GetProfileByUserId(
            _db, userId);

        if (profile == null)
        {
            return NotFound();
        }

        // Update profile values
        profile.SleepSchedule = SleepSchedule;
        profile.Cleanliness = Cleanliness;
        profile.NoiseTolerance = NoiseTolerance;
        profile.IsSmoker = IsSmoker;
        profile.OkWithSmoker = OkWithSmoker;

        // Save changes to SQLite
        _db.SaveChanges();

        return RedirectToPage("/Profile/MyProfile");
    }
}