using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using RoomMate.Data;
using RoomMate.Models;

namespace RoomMate.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly RoomMateDbContext _db;

    public RegisterModel(RoomMateDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [BindProperty]
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [BindProperty]
    [Required]
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = "";

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Check if email already exists
        if (_db.Users.Any(u => u.Email == Email))
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return Page();
        }

        // Create user
        var user = new User
        {
            Email = Email.Trim().ToLowerInvariant()
        };

        // Hash password before saving
        var hasher = new PasswordHasher<User>();
        user.Password = hasher.HashPassword(user, Password);

        _db.Users.Add(user);
        _db.SaveChanges();

        // Automatically log in the new user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity)
        );

        return RedirectToPage("/Profile/Create");
        
    }
}