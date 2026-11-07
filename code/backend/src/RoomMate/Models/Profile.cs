using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomMate.Models;

public partial class Profile
{
    [Key]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [Range(1, 5)]
    public int SleepSchedule { get; set; }

    [Range(1, 5)]
    public int Cleanliness { get; set; }

    [Range(1, 5)]
    public int NoiseTolerance { get; set; }

    // Social interaction level, 1 = very introverted, 5 = very extroverted
    [Range(1, 5)]
    public int SocialInteraction { get; set; }

    // Allergies, true if the user has allergies, false otherwise
    [Required(ErrorMessage = "Please select whether you have allergies.")]
    public bool Allergies { get; set; }

    //Nullable Allergies description, only required if Allergies is true
    [StringLength(500)]
    public string? AllergiesDescription { get; set; }

    [Required(ErrorMessage = "Please select whether you are a smoker.")]
    public bool? IsSmoker { get; set; }

    public bool OkWithSmoker { get; set; } = false;

    // Connect Profile to the temporary User model
    public User User { get; set; } = null!;
}
