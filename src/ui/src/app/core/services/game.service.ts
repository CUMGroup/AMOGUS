import { Injectable } from '@angular/core';
import { question } from "../interfaces/question";

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor() { }

  // mock data

  currentQuestion = 0;

  questions: Array<question> = [
    {
      question: "9+10 ?",
      answer: "21",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "19",
        "20",
        "21"
      ]
    },

    {
      question: "9+11 ?",
      answer: "21",
      time: 10,
      type: "text",
    },

    {
      question: "9+10 ?",
      answer: "21",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "19",
        "20",
        "21"
      ]
    },

    {
      question: "9+10 ?",
      answer: "21",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "19",
        "20",
        "21"
      ]
    },
  ]

  getQuestion() : question{
    if(this.currentQuestion >= this.questions.length){
      //game end state
      return this.questions[0];
    }else{
      return this.questions[this.currentQuestion++];
    }
  }
}
