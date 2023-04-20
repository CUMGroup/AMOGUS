# Backend Refactor - Summary 20.04.2023

## Additional Information
For unit testing we have to mock some services and classes. For that we use the [Moq](https://github.com/moq/moq4) library. To mock methods of a dependency, it has to be an interface. So we split some functions into new Interfaces to be able to test independently. The __Single Responsibility Principle__ and the __Dependency Inversion Principle__ were used for that. 

## Abstractions

### New / Changed Interfaces
- [`ITokenFactory`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Security/ITokenFactory.cs) <br> Added some Methods used in the authentication process to create JWT Tokens with custom Claims. Authentication can now be mocked when Unit Tested.
- [`IRoleManager`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Database/IRoleManager.cs) <br> This is a wrapper for the ASP.Net Identity `RoleManager<TRole>` object. 
- [`IUserManager`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Database/IUserManager.cs) <br> This is a wrapper for the ASP.Net Identity `UserManager<TUser>` object.
- [`IQuestionFileAccessor`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Game/IQuestionFileAccessor.cs)<br> This is used as an abstraction for the file-access used to load and save the questions. This has to be mocked to be system independent.
- [`IStatsService`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Game/IStatsService.cs)<br> Moved some functionality from `IGameService` to `IStatsService`.
- [`IDateTime`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Abstractions/IDateTime.cs)<br>Abstraction for the System Clock.
- Added multiple interfaces to access environment variables which wrap the ASP.Net `IConfiguration` object ([`IJwtConfiguration`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Configuration/IJwtConfiguration.cs), [`IQuestionRepoConfiguration`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Configuration/IQuestionRepoConfiguration.cs)). Configuration variables can now be mocked.

### Repositories
- Removed Database access from all services and abstracted it via Repository Interfaces. These can now be mocked.
	- [`IGameSessionRepository`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Repositories/IGameSessionRepository.cs)
	- [`IUserStatsRepository`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Repositories/IUserStatsRepository.cs)
	- [`IUserMedalRepository`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Interfaces/Repositories/IUserMedalRepository.cs)

### Helpers
- Introduced the [`Result<T>`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Common/Communication/Result.cs) struct. Service methods can now return a `Result<T>` which either represents the value of type `T` or an `Exception`. This avoids Exception throwing (which in my opinion is bad style in this case) and makes the return of methods more meaningful and simple.
- Added some new `Exception` types.
## Code Changes
- Refactored the [`AuthService`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Infrastructure/Services/User/AuthService.cs) to increase testability of the methods.
- Refactored the [`UserService`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Infrastructure/Services/User/UserService.cs) to have smaller Methods and better code style in general.
- Refactored the [`ExerciseService`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Services/Gameplay/ExerciseService.cs) - replaced file-access methods with `IQuestionsFileAccessor` and refactored the methods.
- Refactored the [`GameService`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/Services/Gameplay/GameService.cs) - used DRY to refactor a lot and moved some functionality to `IStatsService`.

## Formatter

We finally introduced an [`.editorconfig`](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/.editorconfig) which gives us the ability to quickly clean up our code (sort usings, proper indentations...) using the inbuilt formatter in Visual Studio.

## Misc
- Seperated the ExtensionMethods in the DependecyInjection files (see [AMOGUS.Core](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Core/DependencyInjection.cs), [AMOGUS.Infrastructure](https://github.com/CUMGroup/AMOGUS/blob/main/src/api/AMOGUS.Infrastructure/DependencyInjection.cs)) to make them shorter and more cohesive.
<br><br>
#### Reference
For reference see the Pull Requests: [#70](https://github.com/CUMGroup/AMOGUS/pull/70), [#71](https://github.com/CUMGroup/AMOGUS/pull/71), [#74](https://github.com/CUMGroup/AMOGUS/pull/74).
