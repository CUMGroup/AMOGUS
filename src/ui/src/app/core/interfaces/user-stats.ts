import { TimeSpan } from "../types/time-span";

export interface UserStats {

    Level: Number;
    CurrentStreak: Number;

    OverallAnswered: Number;
    CorrectAnswers: Number;

    TotalTimePlayed: TimeSpan;
    QuickestAnswer: TimeSpan;
    SlowestAnswer: TimeSpan;

    LongestStreak: Number;

    CategorieAnswers: Map<String, Number>;
    CorrectPerDay: Number[];
}