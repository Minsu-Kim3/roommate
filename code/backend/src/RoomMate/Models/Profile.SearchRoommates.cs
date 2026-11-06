using Microsoft.EntityFrameworkCore;
using RoomMate.Data;

namespace RoomMate.Models;

public partial class Profile
{
    //returns an empty list if this user has no profile or it is not complete.
    public static List<RoommateMatch> SearchRoommates(RoomMateDbContext db, int userId, double minScore = 0){
        if (minScore < 0 || minScore > 100){
            throw new ArgumentOutOfRangeException(nameof(minScore), "minScore has to be between 0 and 100");
        }
        //this is what returns the empty list as the questionmark allows for the null state
        Profile? myProfile = GetProfileByUserId(db, userId);

        if (myProfile == null || !myProfile.IsComplete()){
            return new List<RoommateMatch>();
        }

        //load every other profile along with its user (for the email)
        List<Profile> otherProfiles = db.Profiles
            .Include(p => p.User)
            .Where(p => p.UserId != userId)
            .ToList();
        List<RoommateMatch> results = new List<RoommateMatch>();

        foreach (Profile other in otherProfiles){
            //cant score someone who has an inclompleate profile
            if (!other.IsComplete()){
                continue;
            }

            //round the score so that we have simple math
            double score = Math.Round(myProfile.CalculateCompatibility(other), 1);

            //0 means deal breaker no match
            if (score <= 0 || score < minScore){
                continue;
            }

            //save the roomate match
            results.Add(new RoommateMatch
            {
                UserId = other.UserId,
                Email = other.User.Email,
                Score = score,
                SleepSchedule = other.SleepSchedule,
                Cleanliness = other.Cleanliness,
                NoiseTolerance = other.NoiseTolerance,
                IsSmoker = other.IsSmoker == true
            });
        }

        //best matches first
        return results
            .OrderByDescending(r => r.Score)
            .ThenBy(r => r.UserId)
            .ToList();
    }
}
