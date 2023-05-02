# Testplan AMOGUS



## Test types

- Unit tests

- API tests

- Beta tests



## Target test coverage

The test coverage is around 60% in terms of unit testing. Goal is to test the services functionalities.

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

### API tests

API tests are run manually

| #   | Route | Objective | Input | Expected Result |
| --- | ----- | --------- | ----- | --------------- |
| 1   |       |           |       |                 |


