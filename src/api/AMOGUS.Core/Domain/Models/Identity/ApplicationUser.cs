using Microsoft.AspNetCore.Identity;

namespace AMOGUS.Infrastructure.Identity {
    public class ApplicationUser : IdentityUser {
        public bool PlayedToday { get; set; }
    }
}
