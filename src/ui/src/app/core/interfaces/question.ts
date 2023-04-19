export interface question{
  questionId?: string;
  exercise: string,
  answer: string;
  wrongAnswers: string[];
  help?:string;
  difficultyType: string;
  categoryType:string;
  experiencePoints?: string;
  finished?: boolean;
  multipleChoiceAnswers?: string[];
  time?: number;
}

export interface gameOption{
  gameType: string;
}
