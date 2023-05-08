
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Validation.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AMOGUS.Validation {
    public static class DependencyInjection {

        public static IServiceCollection AddValidators(this IServiceCollection services) {

            services.AddSingleton<IValidator<RegisterApiModel>, RegisterValidator>();
            services.AddSingleton<IValidator<UserStats>, StatsValidator>();
            services.AddSingleton<IValidator<GameSession>, GameSessionValidator>();

            return services;
        }

    }
}
