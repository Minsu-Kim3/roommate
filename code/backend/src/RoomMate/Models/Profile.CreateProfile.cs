
using System.ComponentModel.DataAnnotations;

namespace RoomMate.Models;

public partial class Profile
{
    // Temporary storage for profiles
    private static readonly Dictionary<int, Profile> profiles = new();

    public static Profile CreateProfile(
        int userId,
        int sleepSchedule,
        int cleanliness,
        int noiseTolerance,
        bool? isSmoker,
        bool okWithSmoker = false)
    {
        Profile profile = new Profile
        {
            UserId = userId,
            SleepSchedule = sleepSchedule,
            Cleanliness = cleanliness,
            NoiseTolerance = noiseTolerance,
            IsSmoker = isSmoker,
            OkWithSmoker = okWithSmoker
        };

        // Validate all fields, including required selections
        Validator.ValidateObject(
            profile,
            new ValidationContext(profile),
            validateAllProperties: true);

        // Prevent duplicate profiles
        if (profiles.ContainsKey(userId))
        {
            throw new InvalidOperationException("Profile already exists.");
        }

        // Save profile temporarily
        profiles.Add(userId, profile);

        return profile;
    }
}
