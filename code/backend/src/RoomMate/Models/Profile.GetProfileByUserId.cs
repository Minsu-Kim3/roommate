
namespace RoomMate.Models;

public partial class Profile
{
    // Find a profile using its UserId
    public static Profile? GetProfileByUserId(int userId)
    {
        if (profiles.TryGetValue(userId, out Profile? profile))
        {
            return profile;
        }

        return null;
    }
}
