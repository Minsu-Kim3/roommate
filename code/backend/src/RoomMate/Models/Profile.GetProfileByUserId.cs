using RoomMate.Data;

namespace RoomMate.Models;

public partial class Profile
{
    public static Profile? GetProfileByUserId(
        RoomMateDbContext db, int userId)
    {
        // Find a profile by UserId
        return db.Profiles.Find(userId);
    }
}
