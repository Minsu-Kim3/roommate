using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RoomMate.Data;
using RoomMate.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Cookie authentication
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

builder.Services.AddDbContext<RoomMateDbContext>(options =>
    options.UseSqlite("Data Source=roommate.db"));

var app = builder.Build();

// Create SQLite database for development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider
        .GetRequiredService<RoomMateDbContext>();

    db.Database.EnsureCreated();

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
