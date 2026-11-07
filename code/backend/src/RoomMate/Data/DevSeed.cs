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

        // email, sleep, cleanliness, noise, sociability, allergies, allergies description, smoker, ok with smoker
        var samples = new (string Email, int Sleep, int Clean, int Noise, int Sociability, bool Allergic, string? AllergiesDescription, bool Smoker, bool OkWithSmoker)[]{
            ("sample1@wsu.edu", 3, 4, 2, 3, false, null , false, false),
            ("sample2@wsu.edu", 1, 2, 5, 5, false, null, false, true),
            ("sample3@wsu.edu", 5, 5, 1, 2, true, "Peanuts, Food Coloring" , false, false),
            ("sample4@wsu.edu", 2, 3, 3, 4, false, null, true, true),
            ("sample5@wsu.edu", 4, 4, 3, 3, true, "Shellfish", false, false)
        };
        //create user profile
        foreach (var s in samples){
            var user = new User{
                Email = s.Email,
                Password = "dev-only"
            };

            db.Users.Add(user);
            db.SaveChanges();

            Profile.CreateProfile(db, user.UserId, s.Sleep, s.Clean, s.Noise, s.Sociability, s.Allergic, s.AllergiesDescription, s.Smoker, s.OkWithSmoker);
        }
    }
}