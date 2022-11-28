using AMOGUS.Core.Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Core.DataTransferObjects.User {
    public class LoginResultApiModel {
        public Result Result { get; private set; }

        public string? Token { get; private set; } = null;

        public DateTime? Expiration { get; private set; } = null;


        public LoginResultApiModel() {
            this.Result = Result.Failure("Login failed");
        }

        public LoginResultApiModel(Result res) {
            this.Result = res;
        }

        public LoginResultApiModel(Result res, string token, DateTime expiration) {
            this.Result = res;
            this.Token = token;
            this.Expiration = expiration;
        }
    }
}
