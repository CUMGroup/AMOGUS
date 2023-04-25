using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Game;

namespace AMOGUS.Api.BackgroundWorker {
    public class StreakUpdateScheduler : BackgroundService {

        private readonly IStreakService _streakService;
        private readonly IDateTime _dateTime;

        private readonly ILogger<StreakUpdateScheduler> _logger;

        public StreakUpdateScheduler(IStreakService streakService, IDateTime dateTime, ILogger<StreakUpdateScheduler> logger) {
            _streakService = streakService!;
            _dateTime = dateTime!;
            _logger = logger!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        
            while(!stoppingToken.IsCancellationRequested) {
                var millis = GetMillisecondsToMidnight();
                await Task.Delay(millis, stoppingToken);

                if (_logger.IsEnabled(LogLevel.Information)) {
                    _logger.LogInformation("Updating Streak for all users");
                }
                await _streakService.UpdateAllStreaksAsync();
            }
        }

        private int GetMillisecondsToMidnight() =>
            (int) (_dateTime.Now.Date.AddDays(1) - _dateTime.Now).TotalMilliseconds;
    }
}
