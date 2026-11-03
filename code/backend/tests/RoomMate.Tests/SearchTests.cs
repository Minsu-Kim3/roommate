using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomMate.Data;
using RoomMate.Models;
using Xunit;

namespace RoomMate.Tests;

public class SearchTests
{
    // Create a database for each test
    private static RoomMateDbContext CreateDatabase(SqliteConnection connection){
        connection.Open();
        var options = new DbContextOptionsBuilder<RoomMateDbContext>().UseSqlite(connection).Options;
        var db = new RoomMateDbContext(options);
        db.Database.EnsureCreated();
        return db;
    }

    // Add a user with a complete profile
    private static void AddStudent(RoomMateDbContext db, int id, int sleep, int clean, int noise, bool smoker, bool okWithSmoker = false){
        db.Users.Add(new TempUser
        {
            UserId = id,
            Email = $"student{id}@wsu.edu",
            Password = "test-only-placeholder"
        });
        db.SaveChanges();

        Profile.CreateProfile(db, id, sleep, clean, noise, smoker, okWithSmoker);
    }

    //3/3/3 non smoker expected scores:
    //   2: 3/3/3          -> 100
    //   3: 1/3/5          -> 66.7
    //   4: smoker         -> 0 (I'm not ok with smokers)
    //   5: 5/5/5          -> 50
    private static void AddStandardStudents(RoomMateDbContext db)
    {
        AddStudent(db, 1, 3, 3, 3, false);
        AddStudent(db, 2, 3, 3, 3, false);
        AddStudent(db, 3, 1, 3, 5, false);
        AddStudent(db, 4, 3, 3, 3, true, true);
        AddStudent(db, 5, 5, 5, 5, false);
    }

    [Fact]
    public void SearchRoommates_SortedByScore(){
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);
        AddStandardStudents(db);
        var results = Profile.SearchRoommates(db, 1);
        Assert.Equal(new[] { 2, 3, 5 }, results.Select(r => r.UserId));
        Assert.Equal(100, results[0].Score);
        Assert.Equal(66.7, results[1].Score);
        Assert.Equal(50, results[2].Score);
        Assert.Equal("student2@wsu.edu", results[0].Email);
    }

    [Fact]
    public void SearchRoommates_ExcludesSelf(){
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);
        AddStandardStudents(db);
        var results = Profile.SearchRoommates(db, 1);
        Assert.DoesNotContain(results, r => r.UserId == 1);
    }

    [Fact]
    public void SearchRoommates_ExcludesSmokingDealbreaker(){
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);
        AddStandardStudents(db);
        var results = Profile.SearchRoommates(db, 1);
        Assert.DoesNotContain(results, r => r.UserId == 4);
    }

    [Fact]
    public void SearchRoommates_MinScoreFilters(){
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);
        AddStandardStudents(db);
        var results = Profile.SearchRoommates(db, 1, 60);
        Assert.Equal(new[] { 2, 3 }, results.Select(r => r.UserId));
    }

    [Fact]
    public void SearchRoommates_SkipsIncompleteProfiles(){
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);
        AddStudent(db, 1, 3, 3, 3, false);

        //save an unfinished profile directly (CreateProfile would reject it)
        //sleepSchedule = 0 means the student never rated it.
        db.Users.Add(new TempUser{
            UserId = 2,
            Email = "student2@wsu.edu",
            Password = "test-only-placeholder"
        });
        db.Profiles.Add(new Profile{
            UserId = 2,
            SleepSchedule = 0,
            Cleanliness = 3,
            NoiseTolerance = 3,
            IsSmoker = false
        });
        db.SaveChanges();
        var results = Profile.SearchRoommates(db, 1);
        Assert.Empty(results);
    }

    [Fact]
    public void SearchRoommates_NoProfile_ReturnsEmpty(){
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);
        AddStudent(db, 2, 3, 3, 3, false);
        var results = Profile.SearchRoommates(db, 999);
        Assert.Empty(results);
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(101.0)]
    public void SearchRoommates_InvalidMinScore_Throws(double minScore){
        using var connection = new SqliteConnection("Data Source=:memory:");
        using var db = CreateDatabase(connection);
        AddStandardStudents(db);
        Assert.Throws<ArgumentOutOfRangeException>(() => Profile.SearchRoommates(db, 1, minScore));
    }
}
