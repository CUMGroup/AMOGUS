using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Game;
using Microsoft.Extensions.DependencyInjection;

namespace AMOGUS.Api.BackgroundWorker {
    public class SendMailServiceScheduler : BackgroundService {

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDateTime _dateTime;
        private readonly ILogger<StreakUpdateScheduler> _logger;

        public SendMailServiceScheduler(IDateTime dateTime, IServiceScopeFactory serviceScopeFactory, ILogger<StreakUpdateScheduler> logger) {
            _dateTime = dateTime!;
            _serviceScopeFactory = serviceScopeFactory!;
            _logger = logger!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {

            while (!stoppingToken.IsCancellationRequested) {
                // get Time until 8:00 PM
                var millis = _dateTime.GetMillisecondsUntil(_dateTime.Now.AddDays(1).Date.AddHours(20));
                await Task.Delay(millis, stoppingToken);

                await SendMail();
            }
        }

        private async Task SendMail() {
            using (var scope = _serviceScopeFactory.CreateScope()) {
                var mailerService = scope.ServiceProvider.GetRequiredService<IMailerService>();

                if (_logger.IsEnabled(LogLevel.Information)) {
                    _logger.LogInformation("Send Mail to all Users that not played today!");
                }
                await mailerService.SendMails();
            }
        }

    }
}
