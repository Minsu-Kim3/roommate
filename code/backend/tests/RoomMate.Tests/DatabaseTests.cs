using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomMate.Data;
using RoomMate.Models;
using Xunit;

namespace RoomMate.Tests;

public class DatabaseTests
{
    [Fact]
    public void CreateAndGetProfile()
    {
        // Create a temporary SQLite database
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<RoomMateDbContext>()
            .UseSqlite(connection)
            .Options;

        // Create tables and insert test data
        using (var db = new RoomMateDbContext(options))
        {
            db.Database.EnsureCreated();

            db.Users.Add(new TempUser
            {
                UserId = 1,
                Email = "student@wsu.edu",
                Password = "test-only-placeholder"
            });

            db.SaveChanges();

            Profile.CreateProfile(
                db, 1, 3, 4, 2, false);
        }

        // Read the saved profile using a new DbContext
        using (var db = new RoomMateDbContext(options))
        {
            Profile? profile = Profile.GetProfileByUserId(db, 1);

            Assert.NotNull(profile);
            Assert.Equal(1, profile.UserId);
            Assert.Equal(3, profile.SleepSchedule);
            Assert.Equal(4, profile.Cleanliness);
            Assert.Equal(2, profile.NoiseTolerance);
            Assert.False(profile.IsSmoker);
            Assert.False(profile.OkWithSmoker);
        }
    }

    [Fact]
    public void GetProfileByUserId_NotFound()
    {
        // Create an empty SQLite database
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<RoomMateDbContext>()
            .UseSqlite(connection)
            .Options;

        using var db = new RoomMateDbContext(options);
        db.Database.EnsureCreated();

        // Return null if the profile does not exist
        Profile? profile = Profile.GetProfileByUserId(db, 999);

        Assert.Null(profile);
    }
}
