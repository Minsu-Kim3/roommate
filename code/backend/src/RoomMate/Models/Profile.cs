
using System.ComponentModel.DataAnnotations;

namespace RoomMate.Models;

public partial class Profile
{
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    [Range(1, 5)]
    public int SleepSchedule { get; set; }

    [Range(1, 5)]
    public int Cleanliness { get; set; }

    [Range(1, 5)]
    public int NoiseTolerance { get; set; }

    [Required(ErrorMessage = "Please select whether you are a smoker.")]
    public bool? IsSmoker { get; set; }

    public bool OkWithSmoker { get; set; } = false;
}
