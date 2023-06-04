using AMOGUS.Core.Common.Interfaces.Configuration;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using System.Net.Mail;
using System.Net;
using AMOGUS.Core.Domain.MailText;
using System.Diagnostics.CodeAnalysis;

namespace AMOGUS.Core.Services.Information {

    [ExcludeFromCodeCoverage]
    internal class MailerService : IMailerService {

        private readonly IMailerConfiguration _mailerConfiguration;
        private readonly IUserManager _userManager;
        private readonly IStatsService _statsService;

        public MailerService(IMailerConfiguration mailerConfiguration, IUserManager userManager, IStatsService statsService) {
            _mailerConfiguration = mailerConfiguration;
            _userManager = userManager;
            _statsService = statsService;
        }

        public async Task<bool> SendMail(string senderMail, string username) {
            try {
                using (var mail = new MailMessage()) {
                    mail.From = new MailAddress(_mailerConfiguration.Email);
                    mail.Subject = MailTextStatics.Subject;
                    mail.To.Add(new MailAddress(senderMail));
                    mail.Body = MailTextStatics.CreateWholeMessage(username);
                    mail.IsBodyHtml = true;

                    // send mail via smtp server
                    using (var smtpClient = new SmtpClient(_mailerConfiguration.Hostname, _mailerConfiguration.Port)) {
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(_mailerConfiguration.Email,
                            _mailerConfiguration.Password);
                        await smtpClient.SendMailAsync(mail);
                    }
                }
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public async Task<bool> SendMails() {
            var allUsers = await _userManager.GetAllAsync();

            var mailTasks = new List<Task>();

            foreach (var user in allUsers) {
                var userResult = await _statsService.GetUserStatsAsync(user.Id);
                if (userResult.IsFaulted) { continue; }
                var stats = userResult.Value;
                if (!user.PlayedToday && stats.CurrentStreak >= 1) {
                    Task mailTask = SendMail(user.Email, user.UserName);
                    mailTasks.Add(mailTask);
                }
            }

            await Task.WhenAll(mailTasks);
            return true;
        }
    }
}
