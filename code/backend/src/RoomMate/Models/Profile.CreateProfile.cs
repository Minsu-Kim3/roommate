using System.ComponentModel.DataAnnotations;
using RoomMate.Data;

namespace RoomMate.Models;

public partial class Profile
{
    public static Profile CreateProfile(
        RoomMateDbContext db,
        int userId,
        int sleepSchedule,
        int cleanliness,
        int noiseTolerance,
        int socialInteraction,
        bool allergies,
        string? allergiesDescription,
        bool? isSmoker,
        bool okWithSmoker = false)
    {
        Profile profile = new Profile
        {
            UserId = userId,
            SleepSchedule = sleepSchedule,
            Cleanliness = cleanliness,
            NoiseTolerance = noiseTolerance,
            SocialInteraction = socialInteraction,
            Allergies = allergies,
            AllergiesDescription = allergiesDescription,
            IsSmoker = isSmoker,
            OkWithSmoker = okWithSmoker
        };

        // Validate ratings and required selections
        Validator.ValidateObject(
            profile,
            new ValidationContext(profile),
            validateAllProperties: true);

        // Save profile to SQLite
        db.Profiles.Add(profile);
        db.SaveChanges();

        return profile;
    }
}
