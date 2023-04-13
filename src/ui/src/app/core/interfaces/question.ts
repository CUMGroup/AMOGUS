export interface question{
  question: string,
  answer: string | number;
  time: number;
  type: "text" | "multipleChoice";
  multipleChoiceAnswers?: string[];
  finished?: boolean;
}

export interface gameOption{
  gameType: string;
}
