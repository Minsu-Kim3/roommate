namespace RoomMate.Models;
//delete once the real Profile.cs (issue #2) is pushed
public partial class Profile
{
    public int UserId { get; set; }
    public int SleepSchedule { get; set; }
    public int Cleanliness { get; set; }
    public int NoiseTolerance { get; set; }
    public bool? IsSmoker { get; set; }//This would be a required feild thus we need to know if it is null or not to determine if thy have answered
    public bool OkWithSmoker { get; set; }// not requiered and would be set to false if not answered
}