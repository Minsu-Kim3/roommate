# RoomMate – University Dormitory Roommate Matching System

## Project Overview

RoomMate is a university dormitory roommate matching system designed to help students find compatible roommates based on their living habits and personal preferences.

The system allows students to create accounts, manage their profiles, calculate compatibility scores, search for potential roommates, and send roommate requests.

The goal of RoomMate is to make the roommate selection process easier and help students find suitable roommates.

## Main Features

- **User Registration and Login:** Students can create accounts and log in using their university email.
- **Profile Management:** Students can create and update profiles containing their living preferences.
- **Compatibility Score:** The system calculates compatibility scores based on sleep schedule, cleanliness, noise tolerance, and smoking preferences.
- **Roommate Search:** Students can search for potential roommates based on compatibility.
- **Roommate Requests:** Students can send, accept, or decline roommate requests.
- **Roommate Pairing:** The system creates confirmed roommate pairs when requests are accepted.
- **Admin Management:** Housing administrators can view confirmed roommate pairs.

## Technologies

| Category | Technology |
|---|---|
| Programming Language | C# |
| Backend | ASP.NET Core (.NET 10) |
| Frontend | Razor Pages |
| Database | SQLite |
| Database Integration | Entity Framework Core |
| Testing | xUnit |
| Version Control | Git & GitHub |
| Project Management | GitHub Issues & Kanban Board |

## Development Process

The project follows an **Agile development approach**, with features implemented and tested incrementally during each sprint.

GitHub Issues are used to organize development tasks, and the Kanban board tracks progress through To Do, In Progress, Code Review, Testing, and Done.

Team members develop features in separate Git branches and integrate them into the main branch through Pull Requests.

## Sprint 1 Progress

During Sprint 1, the team focused on establishing the project structure and implementing core backend features.

### Completed Work

- Set up the ASP.NET Core project and GitHub repository.
- Implemented the Profile model with input validation.
- Implemented profile creation and retrieval by UserId.
- Integrated SQLite using Entity Framework Core.
- Established a one-to-one relationship between users and profiles.
- Implemented compatibility score calculation.
- Created unit tests for the implemented features.
- Merged the Profile and Compatibility features into the main branch.
