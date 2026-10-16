using RoomMate.Models;
using Xunit;
namespace RoomMate.Tests;
public class CompatibilityTests{
    //create profiles
    private static Profile Make(int sleep, int clean, int noise, bool? isSmoker, bool okWithSmoker){
        Profile p = new Profile();
        p.SleepSchedule = sleep;
        p.Cleanliness = clean;
        p.NoiseTolerance = noise;
        p.IsSmoker = isSmoker;
        p.OkWithSmoker = okWithSmoker;
        return p;
    }

    //Scenario where both profiles complete and the score is shown
    [Fact]
    public void profile_Score100(){
        double score = Make(3, 4, 2, false, false).CalculateCompatibility(Make(3, 4, 2, false, false));
        //checks the expected values
        Assert.Equal(100.0, score, 2);
    }

    [Fact]
    public void profile_Score0(){
        double score = Make(1, 1, 1, false, false).CalculateCompatibility(Make(5, 5, 5, false, false));
        Assert.Equal(0.0, score, 2);
    }

    [Fact]
    public void MixedProfiles(){
        // sleep 1.0 + clean 0.5 + noise 0.75 = 2.25 / 3 = 75
        double score = Make(3, 2, 4, false, false).CalculateCompatibility(Make(3, 4, 5, false, false));
        Assert.Equal(75.0, score, 2);
    }

    //scenario where there is a incomplete profile expected: no score + message

    [Fact]
    public void MissingRating(){
        Profile incomplete = Make(0, 3, 3, false, false);
        Profile complete = Make(3, 3, 3, false, false);
        //checks if it is the correct error type amd srores it in ex
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(delegate { 
            incomplete.CalculateCompatibility(complete); 
        });
        //checks if the error is right
        Assert.Equal("profile is not completed yet", ex.Message);
    }

    [Fact]
    public void SmokingNotAnswered(){
        Profile incomplete = Make(3, 3, 3, null, false);
        Profile complete = Make(3, 3, 3, false, false);

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
            delegate { 
                incomplete.CalculateCompatibility(complete); 
            });

        Assert.Equal("profile is not completed yet", ex.Message);
    }

    [Fact]
    public void SmokerAndNonSmokerWhoMinds_Score0(){
        double score = Make(3, 3, 3, true, false).CalculateCompatibility(Make(3, 3, 3, false, false));
        Assert.Equal(0.0, score, 2);
    }

    [Fact]
    public void SmokerAndNonSmokerWhoIsOk(){
        double score = Make(3, 3, 3, true, false).CalculateCompatibility(Make(3, 3, 3, false, true));
        Assert.Equal(100.0, score, 2);
    }

    [Fact]
    public void TwoSmokers(){
        double score = Make(3, 3, 3, true, false).CalculateCompatibility(Make(3, 3, 3, true, false));
        Assert.Equal(100.0, score, 2);
    }
}