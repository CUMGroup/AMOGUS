namespace AMOGUS.Core.DataTransferObjects.User {
    public class LeaderboardApiModel {

        public LeaderboardUserStreak[] LongestStreaks { get; set; }
        public LeaderboardUserStreak[] CurrentStreaks { get; set; }

        public LeaderboardUserCorrectRatio[] CorrectRatios { get; set; }

        public LeaderboardApiModel(LeaderboardUserStreak[] LongestStreaks, LeaderboardUserStreak[] CurrentStreaks, LeaderboardUserCorrectRatio[] CorrectRatios) {
            this.LongestStreaks = LongestStreaks;
            this.CurrentStreaks = CurrentStreaks;
            this.CorrectRatios = CorrectRatios;
        }
    }

    public class LeaderboardUserStreak {

        public string Username { get; set; } = string.Empty;
        public int Streak { get; set; }

    }

    public class LeaderboardUserCorrectRatio {

        public string Username { get; set; } = string.Empty;
        public double CorrectRatio { get; set; }
    }
}
