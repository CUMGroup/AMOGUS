using AMOGUS.Core;
using AMOGUS.Core.Centralization.User;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDataServices(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddCoreServices();

builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();

var app = builder.Build();

app.UsePathBase("/api");

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
    await db.EnsureDatabaseAsync();

    var user = scope.ServiceProvider.GetRequiredService<IAuthService>();
    await user.CreateRolesAsync<UserRoles>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseCors(e =>
        e.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(orig => "amogus.alexmiha.de".Equals(orig)));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
