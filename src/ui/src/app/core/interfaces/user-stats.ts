import { TimeSpan } from "../types/time-span";

export interface UserStats {

    Level: number;
    CurrentStreak: number;

    OverallAnswered: number;
    CorrectAnswers: number;

    TotalTimePlayed: TimeSpan;
    QuickestAnswer: TimeSpan;
    SlowestAnswer: TimeSpan;

    LongestStreak: number;

    CategorieAnswers: Map<string, number>;
    CorrectPerDay: number[];

    Unlocked: string[];
    NextPrize: {
        level: number,
        prize: string
    };
}