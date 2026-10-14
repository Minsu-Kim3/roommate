
using System.ComponentModel.DataAnnotations;
using RoomMate.Models;
using Xunit;

namespace RoomMate.Tests;

public class ProfileTests
{
    // Create a profile with valid information
    [Fact]
    public void CreateProfile_Valid()
    {
        Profile profile = Profile.CreateProfile(
            101, 3, 4, 2, false);

        Assert.Equal(101, profile.UserId);
        Assert.Equal(3, profile.SleepSchedule);
        Assert.Equal(4, profile.Cleanliness);
        Assert.Equal(2, profile.NoiseTolerance);
        Assert.False(profile.IsSmoker);
        Assert.False(profile.OkWithSmoker);
    }

    // Rating must be between 1 and 5
    [Fact]
    public void CreateProfile_InvalidRating()
    {
        Assert.Throws<ValidationException>(() =>
            Profile.CreateProfile(102, 0, 4, 2, false));
    }

    // IsSmoker is required
    [Fact]
    public void CreateProfile_SmokingNotAnswered()
    {
        ValidationException ex = Assert.Throws<ValidationException>(() =>
            Profile.CreateProfile(103, 3, 4, 2, null));

        Assert.Equal(
            "Please select whether you are a smoker.",
            ex.Message);
    }

    // A user cannot create two profiles
    [Fact]
    public void CreateProfile_DuplicateUserId()
    {
        Profile.CreateProfile(104, 3, 4, 2, false);

        Assert.Throws<InvalidOperationException>(() =>
            Profile.CreateProfile(104, 2, 3, 5, true));
    }

    // Get a profile using its UserId
    [Fact]
    public void GetProfileByUserId_Exists()
    {
        Profile.CreateProfile(105, 3, 4, 2, false);

        Profile? profile = Profile.GetProfileByUserId(105);

        Assert.NotNull(profile);
        Assert.Equal(105, profile.UserId);
        Assert.Equal(4, profile.Cleanliness);
    }

    // Return null if the profile does not exist
    [Fact]
    public void GetProfileByUserId_NotFound()
    {
        Profile? profile = Profile.GetProfileByUserId(-999);

        Assert.Null(profile);
    }
}
