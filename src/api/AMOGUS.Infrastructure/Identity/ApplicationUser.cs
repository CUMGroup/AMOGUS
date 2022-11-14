using AMOGUS.Core.Common.Interfaces.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Infrastructure.Identity {
    internal class ApplicationUser : IdentityUser, IApplicationUser {
        public bool PlayedToday { get; set; }
    }
}
