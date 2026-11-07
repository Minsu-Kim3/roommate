// Loads and updates the user's profile in the database.
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;

namespace RoomMate.Pages.Profile;

[Authorize]
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
    [Range(1, 5)]
    public int SocialInteraction { get; set; }

    [BindProperty]
    [Required]
    public bool Allergies { get; set; }

    [BindProperty]
    [StringLength(500)]
    public string? AllergiesDescription { get; set; }

    [BindProperty]
    [Required]
    public bool? IsSmoker { get; set; }

    [BindProperty]
    public bool OkWithSmoker { get; set; }

    public IActionResult OnGet()
    {
        // Get logged-in user's ID
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userId))
        {
            return Challenge();
        }

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
        SocialInteraction = profile.SocialInteraction;
        Allergies = profile.Allergies;
        AllergiesDescription = profile.AllergiesDescription;
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

        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userId))
        {
            return Challenge();
        }

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
        profile.SocialInteraction = SocialInteraction;
        profile.Allergies = Allergies;
        profile.AllergiesDescription = AllergiesDescription;
        profile.IsSmoker = IsSmoker;
        profile.OkWithSmoker = OkWithSmoker;

        // Save changes to SQLite
        _db.SaveChanges();

        return RedirectToPage("/Profile/MyProfile");
    }
}