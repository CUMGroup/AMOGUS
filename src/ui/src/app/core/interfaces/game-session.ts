import { question } from "./question";

export interface GameSession {

    sessionId : string;
    userId : string;
    playTime: number;
    correctAnswersCount: number;
    givenAnswersCount: number;

    averageTimePerQuestion: number;
    quickestAnswer: number;
    slowestAnswer: number;

    category: CategoryType;

    playedAt: Date;

    questions: question[]
}

export enum CategoryType {
    MENTAL,
    ANALYSIS,
    GEOMETRY
}