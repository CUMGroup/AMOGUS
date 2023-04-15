using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Infrastructure.Identity;
using AMOGUS.Infrastructure.Persistence;
using AMOGUS.Infrastructure.Persistence.User;
using AMOGUS.Infrastructure.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AMOGUS.Infrastructure {
    public static class DependencyInjection {

        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment) {

            services.AddDatabaseContext(configuration, isDevelopment);
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddIdentitiyServices();
            services.AddAuthenticationServices(configuration);

            services.AddTransient<IUserManager, UserManagerWrapper>();
            services.AddTransient<IRoleManager, RoleManagerWrapper>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenFactory, TokenFactory>();

            return services;
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration, bool isDevelopment) {
            if (isDevelopment) {
                services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                            );
            }
            else {
                services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseMySql(configuration.GetConnectionString("DefaultConnection")!,
                                    new MariaDbServerVersion(new Version(configuration["DatabaseConfig:Version"]!)))
                            );
            }
            return services;
        }

        private static IServiceCollection AddIdentitiyServices(this IServiceCollection services) {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();
            return services;
        }

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            return services;
        }
    }
}
