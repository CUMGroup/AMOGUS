export class LeaderboardModel {
    longestStreaks:  LeaderboardUserStreak[];
    currentStreaks:  LeaderboardUserStreak[];
    correctRatios:  LeaderboardUserCorrectRatio[];
}

export class LeaderboardUserStreak {
    username: string;
    streak: number;

    constructor(username: string, streak: number) {
        this.username = username;
        this.streak = streak;
    }
}

export class LeaderboardUserCorrectRatio {
    username: string;
    correctRatio: number;

    constructor(username: string, correctRatio: number) {
        this.username = username;
        this.correctRatio = correctRatio;
    }
}