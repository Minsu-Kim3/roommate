using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;

namespace RoomMate.Pages.Profile;

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
    [Required]
    public bool? IsSmoker { get; set; }

    [BindProperty]
    public bool OkWithSmoker { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Temporary user until Login is implemented
        int userId = 1;

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
            IsSmoker,
            OkWithSmoker
        );

        return RedirectToPage("/Index");
    }
}