using AMOGUS.Core.Common.Interfaces.Configuration;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Teacher;
using AMOGUS.Core.Services.Gameplay;
using AMOGUS.Core.Services.Information;
using AMOGUS.Core.Services.Teacher;
using Microsoft.Extensions.DependencyInjection;

namespace AMOGUS.Core {
    public static class DependencyInjection {

        public static IServiceCollection AddCoreServices(this IServiceCollection services) {

            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IQuestionFileAccessor, QuestionFileAccessor>();
            services.AddTransient<IStatsService, StatsService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<ILeaderboardService, LeaderboardService>();
            services.AddTransient<IMailerService, MailerService>();

            services.AddTransient<IStreakService, StreaksService>();

            services.AddConfigurations();

            return services;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection services) {
            services.AddTransient<IJwtConfiguration, JwtConfiguration>();
            services.AddTransient<IQuestionRepoConfiguration, QuestionRepoConfiguration>();
            services.AddTransient<IMailerConfiguration, MailerConfiguration>();
            return services;
        }

    }
}
