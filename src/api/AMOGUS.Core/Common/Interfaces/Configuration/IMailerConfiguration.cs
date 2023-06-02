using Microsoft.Extensions.Configuration;

namespace AMOGUS.Core.Common.Interfaces.Configuration {
    public interface IMailerConfiguration {

        string Hostname { get; }
        string Email { get; }
        int Port { get; }
        string Password { get; }

    }

    internal class MailerConfiguration : IMailerConfiguration {

        private readonly IConfiguration _configuration;

        public MailerConfiguration(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string Hostname => _configuration["MailCredentials:Hostname"]!;

        public string Email => _configuration["MailCredentials:Email"]!;

        public int Port => int.Parse(_configuration["MailCredentials:Port"]!);

        public string Password => _configuration["MailCredentials:Password"]!;
    }
}
