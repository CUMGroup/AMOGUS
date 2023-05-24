export class NewQuestion {
  questionId: string;
  answer: string;
  category: number;
  difficulty: number;
  exercise: string;
  experiencePoints: number;
  help:string;
  wrongAnswers: string[];

  constructor(questionId: string, exercise: string, answer: string, wrongAnswers: string[], help:string, category: number, difficulty: number, experiencePoints: number) {
    this.questionId = questionId;
    this.exercise = exercise;
    this.answer = answer;
    this.wrongAnswers = wrongAnswers;
    this.help = help;
    this.category = category;
    this.difficulty = difficulty;
    this.experiencePoints = experiencePoints;
  }

  buildNewQuestion(jsonObj) {
    let question = JSON.parse(jsonObj);
    for (let val in question) {
        this[val] = question[val];
    }
  }
}

export enum CategoryTypeNewQuestion {
    MENTAL,
    ANALYSIS,
    GEOMETRY
}

export enum DifficultyTypeNewQuestion {
    EASY,
    MEDIUM,
    HARD
}
