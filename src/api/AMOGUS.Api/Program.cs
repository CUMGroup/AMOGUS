using AMOGUS.Api;
using AMOGUS.Api.BackgroundWorker;
using AMOGUS.Core;
using AMOGUS.Infrastructure;
using AMOGUS.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add env vars
builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

// Add all service packages
builder.Services.AddValidators();
builder.Services.AddDataServices(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddCoreServices();

builder.Services.AddHostedService<StreakUpdateScheduler>();


var app = builder.Build();

app.UsePathBase("/api");

await app.EnsureDatabaseOnStartupAsync();


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
