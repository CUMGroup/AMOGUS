namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IStreakService {

        Task UpdateAllStreaksAsync();

        Task<int> ReadStreakAsync(string userId);
    }
}
