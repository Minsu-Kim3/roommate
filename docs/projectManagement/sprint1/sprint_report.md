# Sprint 1 Report
Video Link:
## What's New (User Facing)
* Profile Model
* Create Profile functionality
* Get Profile by User ID functionality
* Compatibility score functionality
* SQLite profile integration
## Work Summary (Developer Facing)
During Sprint 1, we set up the ASP.NET Core project using C# and .NET. We used GitHub branches, issues, and a Kanban board to manage tasks and collaborate. We developed the Profile features, including creating user profiles, retrieving profiles by user ID, and calculating compatibility scores. We also created UML diagrams to design and understand the system structure and interactions.
## Design Decisions
- Built as a web app with ASP.NET Core Razor Pages so students can use it from any browser.
- Used SQLite with Entity Framework Core because it is a single file and needs no database server.
- Split the Profile class into partial class files so teammates could work on different methods without merge conflicts.
- Smoking is a dealbreaker filter, not a rating. If two students are smoking-incompatible the score is 0. Otherwise the score comes from the sleep schedule, cleanliness, and noise tolerance ratings.
- TempUser is a placeholder so profiles can be saved and tested until the real User class is built in Sprint 2.
- More detail in [02_Design.md](../../reports/02_Design.md)
## Testing
- Unit tests are written with xUnit. There are 16 tests and all pass on main.
- They cover the compatibility score (score range, smoking filter, incomplete profiles), profile creation and validation, getting a profile by user ID, and saving and loading profiles in SQLite.
- Full list in [03_TestPlan.md](../../reports/03_TestPlan.md)
## Unfinished Work
We were unable to complete the User class, User Registration, and User Login functionality during Sprint 1. These tasks will be carried over to Sprint 2 for implementation and testing.
## Completed Issues/User Stories
The following issues were completed during Sprint 1:
- #3 Calculate Compatibility Score
- #6 ASP.NET Core Setup
- #9 SQLite Database Integration for Profiles
- #2 Create Profile Model
- #4 Create User Profile
- #10 Get Profile by User ID
## Incomplete Issues/User Stories
- #1 User Login — Not completed during Sprint 1 and will be continued in Sprint 2.
- #5 User Registration — Not completed during Sprint 1 and will be continued in Sprint 2.
- User Class — Not completed during Sprint 1 and will be implemented in Sprint 2.
## Code Files for Review
Please review the following code files, which were actively developed during this
sprint, for quality:
* [Profile.Compatibility.cs](https://github.com/Minsu-Kim3/roommate/blob/main/code/backend/src/RoomMate/Models/Profile.Compatibility.cs)
* [CompatibilityTests.cs](https://github.com/Minsu-Kim3/roommate/blob/main/code/backend/tests/RoomMate.Tests/CompatibilityTests.cs)
* [Profile.CreateProfile.cs](https://github.com/Minsu-Kim3/roommate/blob/main/code/backend/src/RoomMate/Models/Profile.CreateProfile.cs)
* [ProfileTests.cs](https://github.com/Minsu-Kim3/roommate/blob/main/code/backend/tests/RoomMate.Tests/ProfileTests.cs)
## Retrospective Summary
# Here's what went well:
* Successfully set up the ASP.NET Core project.
* Implemented profile features and SQLite database integration.
* Implemented calculate compatibility score based on two profiles. 
# Here's what we'd like to improve:
* Improve team communication and task distribution.
* Update the Kanban Board more consistently.
* Allocate more time for testing and integration.
# Here are changes we plan to implement in the next sprint:
* Complete the User class, User Registration, and User Login features.
* Connect the database and backend functionality to the website.
* Implement roommate searching and compatibility-based matching features.