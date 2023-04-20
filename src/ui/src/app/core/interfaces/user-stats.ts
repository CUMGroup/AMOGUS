import { TimeSpan } from "../types/time-span";

export interface UserStats {

    level: number;
    currentStreak: number;

    overallAnswered: number;
    correctAnswers: number;

    totalTimePlayed: number;
    quickestAnswer: number;
    slowestAnswer: number;

    longestStreak: number;

    categorieAnswers: Map<string, number>;
    correctAnswersPerDay: Map<Date, number>;
}