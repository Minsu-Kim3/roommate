namespace RoomMate.Models;

// One row in the roommate search results
public class RoommateMatch
{
    public int UserId { get; set; }

    public string Email { get; set; } = "";

    // Compatibility score from 0 to 100
    public double Score { get; set; }

    public int SleepSchedule { get; set; }

    public int Cleanliness { get; set; }

    public int NoiseTolerance { get; set; }

    public bool IsSmoker { get; set; }
}
