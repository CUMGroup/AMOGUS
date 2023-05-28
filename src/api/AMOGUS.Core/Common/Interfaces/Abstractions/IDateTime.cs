namespace AMOGUS.Core.Common.Interfaces.Abstractions {
    public interface IDateTime {

        DateTime Now { get; }

        int GetMillisecondsUntil(DateTime timeTo);
    }
}
