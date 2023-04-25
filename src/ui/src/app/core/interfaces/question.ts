import { CategoryType } from "./game-session";

export class question{
  answer: string;
  category:string;
  difficulty: number;
  exercise: string;
  experiencePoints: number;
  help:string;
  questionId?: string;
  wrongAnswers: string[];
  finished?: boolean;

  constructor(answer: string, category: string, difficulty: number, exercise: string, experiencePoints: number, help:string, questionId: string, wrongAnswers: string[], finished: boolean = false) {
    this.answer = answer;
    this.category = category;
    this.difficulty = difficulty;
    this.exercise = exercise;
    this.experiencePoints = experiencePoints;
    this.help = help;
    this.questionId = questionId;
    this.wrongAnswers = wrongAnswers;
    this.finished = finished;
  }

  getTime() : number {
    return (this.difficulty + 1) * 10;
  }

  private answersInRandOrder? : string[];

  getMultipleChoiceAnswers() : string[] {
    if(!this.answersInRandOrder) {
      this.answersInRandOrder = (Array.of(...this.wrongAnswers, this.answer))
        .map(value => ({ value, sort: Math.random() }))
        .sort((a, b) => a.sort - b.sort)
        .map(({ value }) => value);
    }
    return this.answersInRandOrder;
  }

  static finished() : question {
    return new question("", "", 0, "", 0, "", "", [], true);
  }
}

export interface gameOption{
  gameType: string;
  category: CategoryType;
}
