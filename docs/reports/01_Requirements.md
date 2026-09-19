## 1. Functional Requirements(FRs)
- FR-01: The system shall allow users to create a new account with an university email address and password. 
- FR-02: The system shall allow registered students to log in the system with their university email address and password
- FR-03: The system shall allow users to update their profile information. 
- FR-04: The system shall calculate the compatibility score between two users based on their profile preference ratings. 
- FR-05: The system shall allow users to search for potential roommates who has similar compatibility score in the roommate matching system. 
- FR-06: The system shall allow users to send roommate requests to another users in the searching lists. 
- FR-07: The system shall notify a student when they receive a roommate request. 
- FR-08: The system shall allow users to accept or decline the roommate requests that the users received. 
## 2. Non-Functional Requirements(NFRs)
- NFR-01: The system shall show the roommate search results within 3 seconds under normal load conditions. 
- NFR-02: The system shall support up to 500 concurrent users without performance degradation. 
- NFR-03: The system shall calculate the compatibility score within 3 seconds after the profile is selected. 
- NFR-04: The system shall allow users to complete the creation of an account process within 3 minutes. 
- NFR-05: The system shall store the user profile data after log out. 
## 3. User Stories 
- US-01: As a student, I want to create an account with my university email and log in with the email and password so that I can access the roommate matching system. 
- US-02: As a student, I want to update my profile so that the system can calculate my compatibility with other students. 
- US-03: As a student, I want to search for compatible roommates and send a roommate request so that I can find a compatible roommate. 
- US-04: As a student, I want to receive the notifications that I receive the roommate requests from other students, so I can accept or decline the requests.
- US-05: As a housing administrator, I want to view the confirmed roommate paris, so I can manage the roommates pairs. 
## 4. Gherkin-Based Acceptance Scenario 

## Feature: Login the system 
### Scenario: Successful account creation and login 
```gherkin
Given I am a student who has a valid university email address
When I create the RoomMate system account with the university email and password 
And I log in the system with the registered email and password 
Then I should be able to access the RoomMate matching system with my account
```
### Scenario: Login with incorrect information
```gherkin
Given I am a student who has a registered RoomMate account
When I enter an incorrect university email or password
And I try to log in the system
Then I should not be able to access the RoomMate matching system
And I see an error message “Wrong Information” 
```

## Feature: Profile and compatibility score 
### Scenario: Successful compatibility score calculation 
```gherkin
Given I am a student who has completed my profile update
And another student has completed their profile update
When I select the other student's profile 
Then I should be able to see the compatibility score between us 
```
### Scenario: Compatibility score with an incomplete profile 
```gherkin
Given I have not completed my profile update
When I try to check the compatibility score with another student 
Then the system should not calculate the compatibility score
And  I see a message “my profile is not completed”
```

## Feature: Search and send roommate request 
### Scenario: Successfully search and send a roommate request 
```gherkin
Given I am logged in the RoomMate system 
When I search for potential roommates 
And I select a student from the search results 
And I send a roommate request to the student 
Then the roommate request should be sent successfully 
```
### Scenario: Send a duplicate roommate request 
```gherkin
Given I have already sent a roommate request to another student 
When I try to send another roommate request to the same student 
Then the roommate request should not be sent again
And I should see a message “ request has already been sent” 
```

## Feature: Manage roommate requests 
### Scenario: Accept a roommate request  
```gherkin
Given I have received a roommate request from another student 
When I accept the roommate request 
Then I should be paired with the student as a confirmed paired roommate 
```
### Scenario: Decline a roommate request 
```gherkin
Given I have received a roommate request from another student 
When I decline the roommate request 
Then I should not be paired with the student
And the roommate request should be declined 
```

## Feature: View confirmed roommate pairs 
### Scenario: Successfully view confirmed roommate pairs  
```gherkin
Given I am a housing administrator who is logged in the RoomMate system 
And there are confirmed roommate pairs 
When I open the confirmed roommate pairs page 
Then I should be able to view the confirmed roommate pairs 
```
### Scenario: No confirmed roommate pairs 
```gherkin
Given I am a housing administrator who is logged in the RoomMate system 
And there are no confirmed roommate pairs 
When I open the confirmed roommate pairs page 
Then I should see a message “no confirmed roommate pairs”  
```