using Microsoft.Extensions.Configuration;

namespace AMOGUS.Core.Common.Interfaces.Configuration {
    public interface IJwtConfiguration {

        string Issuer { get; }
        string Audience { get; }
        string Key { get; }

    }

    internal class JwtConfiguration : IJwtConfiguration {

        private readonly IConfiguration _configuration;

        public JwtConfiguration(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string Issuer => _configuration["Jwt:Issuer"]!;

        public string Audience => _configuration["Jwt:Audience"]!;

        public string Key => _configuration["Jwt:Key"]!;
    }
}
