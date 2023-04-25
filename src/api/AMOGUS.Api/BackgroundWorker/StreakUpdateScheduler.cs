using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Game;

namespace AMOGUS.Api.BackgroundWorker {
    public class StreakUpdateScheduler : BackgroundService {

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDateTime _dateTime;
        private readonly ILogger<StreakUpdateScheduler> _logger;

        public StreakUpdateScheduler(IDateTime dateTime, IServiceScopeFactory serviceScopeFactory, ILogger<StreakUpdateScheduler> logger) {
            _dateTime = dateTime!;
            _serviceScopeFactory = serviceScopeFactory!;
            _logger = logger!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {

            while (!stoppingToken.IsCancellationRequested) {
                var millis = GetMillisecondsToMidnight();
                await Task.Delay(millis, stoppingToken);

                await UpdateStreaksAsync();
            }
        }

        private async Task UpdateStreaksAsync() {
            using (var scope = _serviceScopeFactory.CreateScope()) {
                var streakService = scope.ServiceProvider.GetRequiredService<IStreakService>();

                if (_logger.IsEnabled(LogLevel.Information)) {
                    _logger.LogInformation("Updating Streak for all users");
                }
                await streakService.UpdateAllStreaksAsync();
            }
        }

        private int GetMillisecondsToMidnight() =>
            (int) (_dateTime.Now.Date.AddDays(1) - _dateTime.Now).TotalMilliseconds;
    }
}
