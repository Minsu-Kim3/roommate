using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;

namespace RoomMate.Pages.Profile;
[Authorize]
public class CreateModel : PageModel
{
    private readonly RoomMateDbContext _db;

    public CreateModel(RoomMateDbContext db)
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
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userId))
        {
            return Challenge();
        }

        if (_db.Profiles.Any(p => p.UserId == userId))
        {
            return RedirectToPage("/Profile/MyProfile");
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Get the user ID from the claims
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userId))
        {
            return Challenge();
        }

        // Check if user exists
        if (_db.Users.Find(userId) == null)
        {
            ModelState.AddModelError("", "User not found.");
            return Page();
        }

        // Prevent duplicate profiles
        if (_db.Profiles.Find(userId) != null)
        {
            ModelState.AddModelError("", "Profile already exists.");
            return Page();
        }

        // Save profile to SQLite
        RoomMate.Models.Profile.CreateProfile(
            _db,
            userId,
            SleepSchedule,
            Cleanliness,
            NoiseTolerance,
            SocialInteraction,
            Allergies,
            AllergiesDescription,
            IsSmoker,
            OkWithSmoker
        );
    
        return RedirectToPage("/Index");
    }
}