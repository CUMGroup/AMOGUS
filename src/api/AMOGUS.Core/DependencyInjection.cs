using AMOGUS.Core.Common.Interfaces.Configuration;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Services.Gameplay;
using Microsoft.Extensions.DependencyInjection;

namespace AMOGUS.Core {
    public static class DependencyInjection {

        public static IServiceCollection AddCoreServices(this IServiceCollection services) {

            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IQuestionFileAccessor, QuestionFileAccessor>();
            services.AddTransient<IStatsService, StatsService>();

            services.AddConfigurations();

            return services;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection services) {
            services.AddTransient<IJwtConfiguration, JwtConfiguration>();
            services.AddTransient<IQuestionRepoConfiguration, QuestionRepoConfiguration>();
            return services;
        }

    }
}
