using AMOGUS.Core.Common.Interfaces.Abstractions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AMOGUS.Infrastructure.Services.Date {
    [ExcludeFromCodeCoverage]
    internal class DateTimeWrapper : IDateTime {
        public DateTime Now => DateTime.Now;

        public int GetMillisecondsUntil(DateTime timeTo) {
            return (int) (timeTo - Now).TotalMilliseconds;
        }
    }
}
