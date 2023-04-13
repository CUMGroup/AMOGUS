using AMOGUS.Core.Common.Communication;

namespace AMOGUS.Core.DataTransferObjects.User {
    public class LoginResultApiModel {
        public string? Token { get; private set; } = null;

        public DateTime? Expiration { get; private set; } = null;

        public string? Username { get; private set; } = null;

        public string? Email { get; private set; } = null;


        public LoginResultApiModel(string token, DateTime expiration, string Username, string Email) {
            this.Token = token;
            this.Expiration = expiration;
            this.Username = Username;
            this.Email = Email;
        }
    }
}
