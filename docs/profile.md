# RoomMate — Profile Feature

## Overview
The Profile feature allows students to create, view, and update their roommate preferences.

## Attributes  
| Attribute | Type | Description |  
| UserId | int | Identifies the student account |  
| SleepSchedule | int (1–5) | Student's sleep schedule |  
| Cleanliness | int (1–5) | Student's cleanliness preference |  
| NoiseTolerance | int (1–5) | Student's noise tolerance |  
| IsSmoker | bool? | Required; null means unanswered |  
| OkWithSmoker | bool | Optional; defaults to false |

## Planned Methods
- CreateProfile()
- GetProfileByUserId()
- UpdateProfile()
- DeleteProfile()

## Validation
- SleepSchedule: 1–5
- Cleanliness: 1–5
- NoiseTolerance: 1–5
- IsSmoker: Required (Yes/No)
- OkWithSmoker: Optional (blank = false)

## Integration
- Account: UserId must match the Account system.
- Compatibility: Profile data will be used to calculate roommate compatibility.
- Database: SQLite will be used to store profiles.
