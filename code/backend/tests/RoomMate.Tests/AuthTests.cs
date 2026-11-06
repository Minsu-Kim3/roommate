using Microsoft.AspNetCore.Identity;
using RoomMate.Models;
using Xunit;

namespace RoomMate.Tests;

public class AuthTests
{
    // Test that hashing a password does not store the plain text password
    [Fact]
    public void HashPassword_ShouldNotStorePlainText()
    {
        var user = new User { Email = "test@wsu.edu" };
        var hasher = new PasswordHasher<User>();

        string hash = hasher.HashPassword(user, "password123");

        Assert.NotEqual("password123", hash);
    }

    // Verify that the correct password gives a successful result
    [Fact]
    public void VerifyPassword_ShouldAcceptCorrectPassword()
    {
        var user = new User { Email = "test@wsu.edu" };
        var hasher = new PasswordHasher<User>();

        string hash = hasher.HashPassword(user, "password123");

        var result = hasher.VerifyHashedPassword(
            user, hash, "password123");

        Assert.NotEqual(PasswordVerificationResult.Failed, result);
    }

    // Verify that the wrong password gives a failed result
    [Fact]
    public void VerifyPassword_ShouldRejectWrongPassword()
    {
        var user = new User { Email = "test@wsu.edu" };
        var hasher = new PasswordHasher<User>();

        string hash = hasher.HashPassword(user, "password123");

        var result = hasher.VerifyHashedPassword(
            user, hash, "wrongpassword");

        Assert.Equal(PasswordVerificationResult.Failed, result);
    }
}
  