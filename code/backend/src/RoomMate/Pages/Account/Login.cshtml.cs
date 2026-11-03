using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoomMate.Data;
using RoomMate.Models;

namespace RoomMate.Pages.Account;

public class LoginModel : PageModel
{
    private readonly RoomMateDbContext _db;

    public LoginModel(RoomMateDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Find user by email
        var email = Email.Trim().ToLowerInvariant();
        var user = _db.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return Page();
        }

        // Verify hashed password
        var hasher = new PasswordHasher<User>();

        PasswordVerificationResult result;

        try
        {
            result = hasher.VerifyHashedPassword(
                user,
                user.Password,
                Password
            );
        }
        catch (FormatException)
        {
            // Handle old test users with plain-text passwords
            ModelState.AddModelError("", "Invalid email or password.");
            return Page();
        }

        if (result == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return Page();
        }

        // Store user information in authentication cookie
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        return RedirectToPage("/Index");
    }
}