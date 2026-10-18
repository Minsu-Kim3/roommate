## Scope
*Sprint 1 covered: profile creation and validation, retrieval by user ID, SQLite save and load, and the compatibility score. *
In Sprint2 we will implement: login, registration, and UI.*

Tests to be implemented in sprint 2:
- *Login and registration*
- *The web pages / UI*

## Approach
- *Framework: xUnit*
- *Database tests uses SQLite database*
- *How to run:*

```
cd code/backend/tests/RoomMate.Tests
dotnet test
```

## Compatibility Score Tests (CompatibilityTests.cs)
- profile_Score100: identical ratings so expected score of 100
- profile_Score0: ratings 4 apart on everything resulting in a score of 0
- MixedProfiles: partly matching proflies so score of 75
- MissingRating: throws profile is not completed yet
- SmokingNotAnswered: throws profile is not completed yet
- SmokerAndNonSmokerWhoMinds_Score0: returns 0 imediatly
- SmokerAndNonSmokerWhoIsOk: both profiles are identical so gives 100
- TwoSmokers: same as above just also smokes100

## Profile Tests (ProfileTests.cs)
- CreateProfile_Valid: creates profile
- CreateProfile_InvalidRating: rating outside 1 to 5 error
- CreateProfile_SmokingNotAnswered: missing smoking causes an error
- CreateProfile_DuplicateUserId: a second profile for the same user error
- GetProfileByUserId_Exists: an existing ID returns profile
- GetProfileByUserId_NotFound: a missing ID returns null

## Database Tests (DatabaseTests.cs)
- CreateAndGetProfile: a saved profile loads with vals inputed
- GetProfileByUserId_NotFound: returns null

## Results
All 16 tests pass.
![Test results](../design/test_results.png)