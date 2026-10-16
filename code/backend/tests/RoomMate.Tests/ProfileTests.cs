using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomMate.Data;
using RoomMate.Models;
using Xunit;

namespace RoomMate.Tests;

public class ProfileTests
{
    // Create a database for each test
    private static RoomMateDbContext CreateDatabase(
        SqliteConnection connection)
    {
        connection.Open();

        var options = new DbContextOptionsBuilder<RoomMateDbContext>()
            .UseSqlite(connection)
            .Options;

        var db = new RoomMateDbContext(options);
        db.Database.EnsureCreated();

        return db;
    }

    [Fact]
    public void CreateProfile_Valid()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);

        db.Users.Add(new TempUser
        {
            UserId = 101,
            Email = "student101@wsu.edu",
            Password = "test-only-placeholder"
        });
        db.SaveChanges();

        Profile profile = Profile.CreateProfile(
            db, 101, 3, 4, 2, false);

        Assert.Equal(101, profile.UserId);
        Assert.Equal(3, profile.SleepSchedule);
        Assert.Equal(4, profile.Cleanliness);
        Assert.Equal(2, profile.NoiseTolerance);
        Assert.False(profile.IsSmoker);
        Assert.False(profile.OkWithSmoker);
    }

    [Fact]
    public void CreateProfile_InvalidRating()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);

        Assert.Throws<ValidationException>(() =>
            Profile.CreateProfile(db, 102, 0, 4, 2, false));
    }

    [Fact]
    public void CreateProfile_SmokingNotAnswered()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);

        ValidationException ex = Assert.Throws<ValidationException>(() =>
            Profile.CreateProfile(db, 103, 3, 4, 2, null));

        Assert.Equal(
            "Please select whether you are a smoker.",
            ex.Message);
    }

    [Fact]
    public void CreateProfile_DuplicateUserId()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);

        db.Users.Add(new TempUser
        {
            UserId = 104,
            Email = "student104@wsu.edu",
            Password = "test-only-placeholder"
        });
        db.SaveChanges();

        Profile.CreateProfile(db, 104, 3, 4, 2, false);
        // Creating another profile with the same UserId should fail
        Assert.Throws<InvalidOperationException>(() =>
            Profile.CreateProfile(db, 104, 2, 3, 5, true));
    }

    [Fact]
    public void GetProfileByUserId_Exists()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);

        db.Users.Add(new TempUser
        {
            UserId = 105,
            Email = "student105@wsu.edu",
            Password = "test-only-placeholder"
        });
        db.SaveChanges();

        Profile.CreateProfile(db, 105, 3, 4, 2, false);

        Profile? profile = Profile.GetProfileByUserId(db, 105);

        Assert.NotNull(profile);
        Assert.Equal(105, profile.UserId);
        Assert.Equal(4, profile.Cleanliness);
    }

    [Fact]
    public void GetProfileByUserId_NotFound()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);

        Profile? profile = Profile.GetProfileByUserId(db, 999);

        Assert.Null(profile);
    }
}
