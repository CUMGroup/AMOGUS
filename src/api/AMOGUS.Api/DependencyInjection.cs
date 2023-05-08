using AMOGUS.Core.Centralization.User;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.User;
using Microsoft.OpenApi.Models;

namespace AMOGUS.Api {
    public static class DependencyInjection {

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services) {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c => {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            });
            return services;
        }


        public static async Task EnsureDatabaseOnStartupAsync(this WebApplication app) {
            using (var scope = app.Services.CreateScope()) {
                var db = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                await db.EnsureDatabaseAsync();

                var user = scope.ServiceProvider.GetRequiredService<IAuthService>();
                await user.CreateRolesAsync<UserRoles>();
            }
        }
    }
}
