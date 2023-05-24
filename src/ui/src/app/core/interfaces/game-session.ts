import { question } from "./question";
export class GameSession {

    sessionId : string;
    userId : string;
    playTime: number;
    correctAnswersCount: number;
    givenAnswersCount: number;

    averageTimePerQuestion: number;
    quickestAnswer: number;
    slowestAnswer: number;

    category: CategoryType;

    questions: question[]

    constructor(sessionId: string, userId: string, playTime: number, correctAnswersCount: number, givenAnswersCount: number, averageTimePerQuestion: number, quickestAnswer:number, slowestAnswer: number, category: CategoryType, questions: question[]) {
        this.sessionId = sessionId;
        this.userId = userId;
        this.playTime = playTime;
        this.correctAnswersCount = correctAnswersCount;
        this.givenAnswersCount = givenAnswersCount;
        this.averageTimePerQuestion = averageTimePerQuestion;
        this.quickestAnswer = quickestAnswer;
        this.slowestAnswer = slowestAnswer;
        this.category = category;
        this.questions = questions;
    }
}

export enum CategoryType {
    MENTAL,
    ANALYSIS,
    GEOMETRY,
    RANDOMMENTAL,
    RANDOMMENTAL_INSANE
}

