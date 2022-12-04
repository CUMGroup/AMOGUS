using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Core.DataTransferObjects.User {
    public class UserApiModel {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool PlayedToday { get; set; }

        public UserApiModel(string userId, string userName, string email, bool playedToday) {
            UserId = userId;
            UserName = userName;
            Email = email;
            PlayedToday = playedToday;
        }
    }
}
