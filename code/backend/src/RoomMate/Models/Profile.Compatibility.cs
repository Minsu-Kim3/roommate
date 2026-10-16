namespace RoomMate.Models;

public partial class Profile{
    private const int minRating = 1;
    private const int maxRating = 5;

    //Compleate indicates that every rating is filled in form a scale of (1–5)
    //and the smoker question is answered
    public bool IsComplete(){
        if (!InRange(SleepSchedule) || !InRange(Cleanliness) || !InRange(NoiseTolerance)){
            return false;
        }
        //everyone must answer "Are you a smoker?"
        if (IsSmoker == null){
            return false;
        }
        return true;
    }

    //checks if two students can live together as far as smoking goes
    public bool IsSmokingCompatible(Profile other){
        bool iSmoke = IsSmoker == true;
        bool theySmoke = other.IsSmoker == true;
        //neither smokes, or both smoke
        if (iSmoke == theySmoke){
            return true;
        }
        //only I smoke so they have to be okay with it
        if (iSmoke){
            return other.OkWithSmoker == true;
        }
        //other smokes so this person would have to be okay with it
        return OkWithSmoker == true;
    }

    //Takes two profiles. This instance of one and the other one (the profile you want to compare)
    //and returns 0 for not matching at all to 100 same basically
    public double CalculateCompatibility(Profile other){
        //checks weather the other profile exists
        if (other == null){
            throw new ArgumentNullException(nameof(other));
        }
        //checks weather this profile is compleated
        if (!IsComplete()){
            throw new InvalidOperationException("profile is not completed yet");
        }
        //checks weather other profile is compleated
        if (!other.IsComplete()){
            throw new InvalidOperationException("the other students profile is not completed");
        }
        //smoking is either a yes or a no so it is a dealbreaker instead of a similarity
        //if they cant live together because of smoking it is not a match
        if (!IsSmokingCompatible(other)){
            return 0;
        }
        //calculates the similarity scores of each profile preference and adds them up
        double total = 0;
        total += Similarity(SleepSchedule, other.SleepSchedule);
        total += Similarity(Cleanliness, other.Cleanliness);
        total += Similarity(NoiseTolerance, other.NoiseTolerance);
        return total / 3 * 100;
    }
    //checks weather the rating is in range or not
    private static bool InRange(int v){
        if (v >= minRating && v <= maxRating){
            return true;
        }
        return false;
    }

    // Same rating = 1.0 ratings 4 apart = 0.0
    private static double Similarity(int a, int b){
        int gap = Math.Abs(a - b);
        int maxGap = maxRating - minRating;
        return 1.0 - (double)gap / maxGap;
    }
}