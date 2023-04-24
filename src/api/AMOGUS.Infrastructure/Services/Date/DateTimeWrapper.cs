using AMOGUS.Core.Common.Interfaces.Abstractions;

namespace AMOGUS.Infrastructure.Services.Date {
    internal class DateTimeWrapper : IDateTime {
        public DateTime Now => DateTime.Now;
    }
}
