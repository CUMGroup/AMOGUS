
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Services.Gameplay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMOGUS.Core {
    public static class DependencyInjection {

        public static IServiceCollection AddCoreServices(this IServiceCollection services) {

            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IExerciseService, ExerciseService>();
            
            return services;
        }

    }
}
