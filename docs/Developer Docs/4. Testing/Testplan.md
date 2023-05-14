# Testplan AMOGUS



## Test types

- Unit tests

- API tests

- Beta tests



## Target test coverage

The test coverage is around 60% in terms of unit testing line and branch coverage. Goal is to test the services functionalities.

Every Endpoint had been testet via Swagger.



## Testing tools

### Unit testing

- [xUnit](https://github.com/xunit/xunit): Unit test framework

- [Moq](https://github.com/moq/moq4): Mock framework to mock services

### API testing

- [Swagger](https://swagger.io/): API testing suite (manual testing)

### Beta tests

- [Humans](https://en.wikipedia.org/wiki/Human): Some random dudes who will test the application e. g. [Luca Hackl](https://luca.hackl.digital/)

- Test server



## Test case management

Github Actions will show the amount of tests passed and failed per pull request.



## Test cases

### Unit test cases

#### Auth Service

| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 1   | CreateRolesAsync | CreateRolesAsync_WhenGivenRoles_CreatesThem | UserRoles class | All Roles correctly created in the RoleManager |
| 2   | CreateRolesAsync | CreateRolesAsync_WhenGivenRoles_AndRoleAlreadyExists_DoesNotCreateIt | UserRoles class | No roles created in the RoleManager |
| 3   | LoginUserAsync | LoginUserAsync_WhenGivenUserData_AndEmailWrong_Exception | Wrong email | Faulted result |
| 4   | LoginUserAsync | LoginUserAsync_WhenGivenUserData_AndPasswordWrong_Exception | Wrong password | Faulted result |
| 5   | LoginUserAsync | LoginUserAsync_WhenGivenUserData_AndAllCorrect_LoginUser | Correct user data | Successful result and Auth Token |
| 6   | RegisterUserAsync | RegisterUserAsync_WhenValidationFails_Exception | user data with failed validation | Faulted result |
| 7   | RegisterUserAsync | RegisterUserAsync_WhenGivenRegisterModel_AndUserAlreadyExists_Exception | user data that already exists | Faulted result |
| 8   | RegisterUserAsync | RegisterUserAsync_WhenGivenRegisterModel_AndFailedToCreate_Exception | valid data but cant create user | Faulted result |
| 9   | RegisterUserAsync |  RegisterUserAsync_WhenGivenRegisterModel_AndRoleDoesNotExist_Exception | valid user data but non existent role | Faulted result |
| 10   | RegisterUserAsync |  RegisterUserAsync_WhenGivenRegisterModel_AndEverythingIsFine_LoginUser | valid user data and valid role | Successful result and Auth Token |

#### ExerciseService

| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 11   | CheckAnswer | CheckAnswer_WhenGivenAnswer_AndQuestionIsNotRandomMental_<br>AndOrigQuestionNotFound_ReturnsFalse | Non random question, that does not exist | false |
| 12   | CheckAnswer | CheckAnswer_WhenGivenAnswer_AndQuestionIsRandomMental_<br>AndAnswerIsNotCalculable_ReturnsFalse | random question that cannot be calculated | false |
| 13   | CheckAnswer | CheckAnswer_WhenGivenAnswer_AndQuestionIsNotRandomMental_<br>AndQuestionExists_ReturnAnswerIsCorrect | non random question with correct answer | true |
| 14   | CheckAnswer | CheckAnswer_WhenGivenAnswer_AndQuestionIsNotRandomMental_<br>AndQuestionExists_ReturnAnswerIsNotCorrect | non random question with incorrect answer | false |
| 15   | CheckAnswer | CheckAnswer_WhenGivenAnswer_AndQuestionIsRandomMental_<br>ReturnAnswerIsCorrect | random question with correct answer | true |
| 16   | CheckAnswer | CheckAnswer_WhenGivenAnswer_AndQuestionIsRandomMental_<br>ReturnAnswerIsNotCorrect | random question with incorrect answer | false |

#### GameService

| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 17   | NewSession | NewSession_WithoutQuestionAmount_ReturnsSessionWith10Questions | no amount specified | Session with 10 questions |
| 18  | NewSession | NewSession_WithQuestionAmount1_ReturnsSessionWith1Question | amount should be 10 | Session with 10 questions |
| 19   | EndSessionAsync | EndSessionAsync_AndUserNotFound_ReturnsException | Wrong userId | Faulted result |
| 20   | EndSessionAsync | EndSessionAsync_AndUserFound_AndValidationFails_ReturnsException | non valid session model | Faulted result |
| 21   | EndSessionAsync | EndSessionAsync_AndUserFound_AndSessionValid_ButDbNotAffected_ReturnsFaulted | Session cannot be saved | Faulted result |
| 22   | EndSessionAsync | EndSessionAsync_AndUserFound_AndSessionValid_AndDbAffected_ReturnsSuccess | session can be saves | Successful result |

#### UserService

| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 23   | GetUserAsync | GetUserAsync_WhenGivenAUserId_AndNoUserWithThatIdExists_Exception | wrong userId | Faulted result |
| 24  | GetUserAsync | GetUserAsync_WhenGivenAUserId_AndUserWithThatIdExists_ReturnsUser | valid userid | Successful result and user |
| 25   | IsInRoleAsync | IsInRoleAsync_WhenGivenAUserId_AndRole_AndUserNotInRole_ReturnsFalse | userId not in role | false |
| 26   | IsInRoleAsync | IsInRoleAsync_WhenGivenAUserId_AndRole_AndUserInRole_ReturnsTrue | userId in role | true |
| 27   | DeleteUserAsync | DeleteUserAsync_WhenGivenAUserId_AndUserNotFound_ReturnsFaulted | wrong userId | Faulted result |
| 28   | DeleteUserAsync | DeleteUserAsync_WhenGivenAUserId_AndUserManagerCantDelete_Exception | valid userId but userManager cannot delete | Faulted result |
| 29   | DeleteUserAsync | DeleteUserAsync_WhenGivenAUserId_AndEverythingIsFine_ReturnTrue | valid userId | Successful result and user is deleted |

#### TeacherService

| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 30   | AddQuestionAsync | AddQuestionAsync_WhenGivenQuestion_AndEverythingIsFine_AddsQuestionAndReturnsQuestion | valid Question | Successful result and Question |
| 31  | AddQuestionAsync | AddQuestionAsync_WhenGivenQuestion_AndIOException_ReturnsException | empty Question | faulted result |
| 32   | DeleteQuestionByIdAsync | DeleteQuestionByIdAsync_WhenGivenQuestionId_AndQuestionDoesNotExist_ReturnsException | invalid QuestionID | faulted result |
| 33   | DeleteQuestionByIdAsync | DeleteQuestionByIdAsync_WhenGivenQuestionId_AndQuestionFileNotAccessible_ReturnsException | valid QuestionID but not accessible | faulted result |
| 34   | DeleteQuestionByIdAsync | DeleteQuestionByIdAsync_WhenGivenQuestionId_AndEverythingIsFine_ReturnsTrue_AndQuestionDeleted | valid QuestionID | Successful result and question deleted |
| 35   | GetQuestionById | GetQuestionById_WhenGivenQuestionId_AndQuestionDoesNotExist_ReturnsException | invalid QuestionID | Faulted result |
| 36   | GetQuestionById | GetQuestionById_WhenGivenQuestionId_AndQuestionDoesExist_ReturnsQuestion | valid QuestionID | Successful result and question |

#### StreaksService

| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 37   | ReadStreakAsync | ReadStreakAsync_WhenGivenAUserId_AndUserHasNoStats_Exception | UserID with no stats | Faulted result |
| 38  | ReadStreakAsync | ReadStreakAsync_WhenGivenAUserId_AndUserHasStats_ReturnsCurrentStreak | valid userid with stats | Successful result and current streak |
| 39   | UpdateAllStreaksAsync | UpdateAllStreaksAsync_WhenPlayerHasNotPlayedToday_AndNotLongestStreak_StreakIsSetTo0 | player that hasn't played today | streak set to 0 |
| 40   | UpdateAllStreaksAsync | UpdateAllStreaksAsync_WhenPlayerHasNotPlayedToday_AndLongestStreak_StreakIsSetTo0_ButKeepLongest | player that hasn't played today | streak set to 0 and longest streak kept |
| 41   | UpdateAllStreaksAsync | UpdateAllStreaksAsync_WhenPlayerHasPlayedToday_ButNotLongestStreak_StreakIsNotLost | player that has played today | streak is kept |
| 42   | UpdateAllStreaksAsync | UpdateAllStreaksAsync_StreaksOfAllPlayersAreUpdated | multiple players that have not played today | all streaks set to 0 |

#### StatsService

| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 43  | GetUserStatsAsync | GetUserStatsAsync_WhenGivenAUserId_AndUserHasNoStats_Exception | userid that has no stats | Faulted result |
| 44   | GetUserStatsAsync | GetUserStatsAsync_WhenGivenAUserId_AndUserHasStats_ReturnsStats | userId that has stats | Successful result and stats |
| 45   | GetDetailedUserStatsModelAsync | GetDetailedUserStatsModelAsync_WhenGivenAUserId_AndUserHasNoStats_Exception | userid that has no stats | Faulted result |
| 46   | GetDetailedUserStatsModelAsync | GetDetailedUserStatsModelAsync_WhenGivenAUserId_EverythingAlright_ReturnsDetailedUserstats | userId that has stats| Successful result and detailed UserStatsApiModel |
| 47   | UpdateUserStatsAsync(UserStats userStats) | UpdateUserStatsAsync_WhenGivenUserStats_AndValidationFails_ReturnsFalse | invalid UserStats | false |
| 48   | UpdateUserStatsAsync(UserStats userStats) | UpdateUserStatsAsync_WhenGivenUserStats_UpdateInDbFails_ReturnsFalse | valid UserStats but repository failed updating | false |
| 49   | UpdateUserStatsAsync(UserStats userStats) | UpdateUserStatsAsync_WhenGivenUserStats_AndEverythingIsFine_ReturnsTrue | valid UserStats | true |
| 50   | UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) | UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndValidationFails_ReturnsFalse | invalid GameSession, Answers and user | false |
| 51   | UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) | UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_UpdateInDbFails_ReturnsFalse | valid GameSession, Answers and user but update in db fails | false |
| 52   | UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) | UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndEverythingIsFine_ReturnsTrue | valid GameSession, Answers and user | true |
| 53   | UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) | UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndEverythingIsFine_StatsAreUpdatedCorrectly | valid GameSession, Answers and user | true and stats updated correctly |
| 54   | UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) | UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndEverythingIsFine_StatsAreUpdatedCorrectly2 | valid GameSession, Answers and user (other values than above so other changes happen) | true and stats updated correctly |
| 55   | UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) | UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndUserHasNotPlayedYet_StreakIsUpdated | like the above but playedtoday is false | played today is now true and streak is updated |
| 56   | UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) | UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndUserHasPlayedAlready_StreakIsNotIncreased | like the above but playedtoday is true | streak is not increased |

#### LeaderboardService
| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 57  | GetLeaderboardAsync | GetLeaderboardAsync_ReturnsCorrectObject | UserStats mock | Correct Object hirachy |

#### MentalExerciseFactory
| #   | Tested method | Objective | Input | Expected Result |
| --- | ------------- | --------- | ----- | --------------- |
| 58  | Median | Median_Returns0_ForEmptyArray | Empty Array | 0 |
| 59  | Median | Median_ReturnsFirstElement_ForArrayWithSize1 | Array with 1 Element | The first element |
| 60  | Median | Median_ReturnsAverage_ForArrayWithSize2 | Array with 2 Elements | Average |
| 61  | Median | Median_ReturnsMedian_ForSortedArrayWithEvenCount | Sorted Array with even Elements | Correct median |
| 62  | Median | Median_ReturnsMedian_ForSortedArrayWithOddCount | Sorted Array with Odd Elements | Correct median |
| 63  | Median | Median_ReturnsMedian_ForRandomArrayWithEvenCount | Shuffled Array with even Elements | Correct median |
| 64  | Median | Median_ReturnsMedian_ForRandomArrayWithOddCount | Shuffled Array with Odd Elements | Correct median |
| 65  | CalcAnswer | CalcAnswer_ReturnsEmpty_WithEmptyQuestion | Empty Question | Empty String |
| 66  | CalcAnswer | CalcAnswer_SolvesEquation_WithXInQuestion | Equation with x | set of solutions |
| 67  | CalcAnswer | CalcAnswer_SolvesExpression_WithQuestion | Expressions without x | Correct answer for the expressions |
| 68  | CalcAnswer | CalcAnswer_ReturnsEmpty_ForXInStatement | Expressions with x | empty string |


### API tests

API tests are run manually

| #   | Route | Objective | Input | Expected Result |
| --- | ----- | --------- | ----- | --------------- |
| 1   |       |           |       |                 |


