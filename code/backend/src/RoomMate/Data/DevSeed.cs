using RoomMate.Models;

namespace RoomMate.Data;

//Sample students
public static class DevSeed
{
    public static void AddSampleRoommates(RoomMateDbContext db){
        //seed once
        if (db.Users.Any(u => u.Email == "sample1@wsu.edu")){
            return;
        }

        // email, sleep, cleanliness, noise, smoker, ok with smoker
        var samples = new (string Email, int Sleep, int Clean, int Noise, bool Smoker, bool OkWithSmoker)[]{
            ("sample1@wsu.edu", 3, 4, 2, false, false),
            ("sample2@wsu.edu", 1, 2, 5, false, true),
            ("sample3@wsu.edu", 5, 5, 1, false, false),
            ("sample4@wsu.edu", 2, 3, 3, true, true),
            ("sample5@wsu.edu", 4, 4, 3, false, true)
        };
        //create user profile
        foreach (var s in samples){
            var user = new TempUser{
                Email = s.Email,
                Password = "dev-only"
            };

            db.Users.Add(user);
            db.SaveChanges();

            Profile.CreateProfile(db, user.UserId, s.Sleep, s.Clean, s.Noise, s.Smoker, s.OkWithSmoker);
        }
    }
}
