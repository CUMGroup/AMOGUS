# Metrics

The following document provides an insight into the software metrics of the AMOGUS project. 

For the backend the source code complexity and the class coupling metrics are examined. AMOGUS is a web application, therefore the web application metrics are examined as well to give an insight to the frontend.

Software testing metrics are a result from the testing described in the [testplan](https://cumgroup.github.io/AMOGUS/Developer%20Docs/4.%20Testing/Testplan/).

## 1. Evaluation methods

In this section the methods used to collect the chosen metrics are described.

### 1.1. Source Code Complexity and Class Coupling

This projects source code complexity and class coupling are measured using the [inbuild feauture](https://learn.microsoft.com/en-us/visualstudio/code-quality/code-metrics-values?view=vs-2022) of the IDE Visual Studio 2022. To evaluate these metrics the results from the calculation of the IDE are gathered on a class level. For the assembly level the median of the results of all classes within that assembly is calculated to exclude outliers.

### 1.2. Web Application Metrics

The web application metrics are measured using the build in feature [Lighthouse](https://developer.chrome.com/docs/lighthouse/) in Google Chrome.

For this project this metric is measured in two rounds:
1. Before optimizing images.
2. After optimizing images. 

## 2. Results

In this section the results of the chosen metrics are displayed.

### 2.1. Source Code Complexity and Coupling

RESULTS

### 2.2.  Web Application Metrics

RESULTS

### 2.3. Software Testing Metrics

All about the software testing metrics and the results of the testing can be found in the [Test Report](https://cumgroup.github.io/AMOGUS/Developer%20Docs/4.%20Testing/zTest%20Report/).





|            |Code Complexity|Class Coupling|
|-|-|-|
|AMOGUS 	5	10
|AMOGUS VALIDATION	1	10
|RegisterValidator()	1	10
|StatsValidator()	1	10
|GameSessionValidator()	1	10
|AddValidators(this IServiceCollection) : IServiceCollection	1	2
|AMOGUS INFRASTRUCTURE	6	17
|DateTimeWrapper	3	4
|UserMedalRepository	3	13
|RoleManagerWrapper	3	15
|GameSessionRepository	5	15
|tokenfactory	6	21
|DependecyInjection	6	34
|UserStatsRepository	9	17
|userservice	9	18
|UserManagerWrapper	13	16
|ApplicationDbContext	16	23
|authservice	22	38
|AMOGUS CORE	4	3
|DependencyInjection	2	2
|UserRoles	1	0
|Result	54	11
|Result<A>	69	12
|ResultState	1	0
|AuthFailureException	4	3
|RecordNotFoundException	1	1
|UserOperationException	4	3
|IDateTime	2	1
|IJwtConfiguration	3	0
|IMailerConfiguration	4	0
|IQuestionRepoConfiguration	1	0
|JwtConfiguration	7	2
|MailerConfiguration	9	2
|QuestionRepoConfiguration	3	2
|IApplicationDbContext	13	8
|IRoleManager	2	3
|IUserManager	12	8
|IExerciseFactory	2	1
|IExerciseService	3	3
|IGameService	3	4
|ILeaderboardService	1	2
|ImailerService	2	1
|IQuestionFileAccessor	7	5
|IStatsService	4	6
|IStreakService	2	3
|IGameSessionRepository	4	5
|IUserMedalRepository	2	3
|IUserStatsRepository	7	5
|ITokenFactory	4	6
|ITeacherService	4	5
|IAuthService	3	6
|IUserService	3	4
|LeaderboardApiModel	7	2
|LeaderboardUserCorrectRatio	4	0
|LeaderboardUserStreak	4	0
|LoginApiModel	4	0
|LoginResultApiModel	9	2
|RegisterApiModel	6	0
|UserApiModel	10	1
|UserStatsApiModel	23	8
|CategoryType	1	0
|DifficultyType	1	0
|UserMedalType	1	0
|MailTextStatics	1	0
|GameSession	24	8
|Question	17	3
|UserMedal	10	9
|UserStats	21	6
|MentalExerciseModel	16	2
|MentalExerciseFactory	41	20
|ExerciseService	16	12
|GameService	9	23
|QuestionFileAccessor	16	24
|StatsService	24	29
|StreaksService	8	10
|LeaderboardService	8	14
|MailerService	7	19
|TeacherService	7	17
|ApplicationUser	2	1
|AMOGUS BENCHMARKS	7	6
|Program	2	3
|MentalExerciseFactoryBenchmarks	7	6
|ExerciseServiceBenchmarks	29	21
|AMOGUS API	5	19
|Program	4	36
|UserController	14	25
|TeacherController	7	23
|StatsController	8	19
|InformationController	2	10
|HealthController	1	6
|GameController	10	24
|AuthController	3	15
|StreakUpdateScheduler	5	14
|SendMailServiceScheduler	5	16
|DependencyInjection	2	23
