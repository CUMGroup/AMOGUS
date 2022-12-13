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
      question: "Find the sum of 9+10",
      answer: "21",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "19",
        "20",
        "21",
        "91"
      ]
    },

    {
      question: "Find the sum of 111+222+333",
      answer: "666",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "700",
        "666",
        "10",
        "100"
      ]
    },

    {
      question: "Subtract 457 from 832",
      answer: "375",
      time: 15,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "375",
        "57",
        "376",
        "970"
      ]
    },
    {
      question: "50 times 5 is equal to",
      answer: "None of these",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "2500",
        "505",
        "500",
        "None of these"
      ]
    },
    {
      question: "Find the product of 72 × 3",
      answer: "216",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "216",
        "7230",
        "106",
        "372"
      ]
    },
    {
      question: "Solve 200 – (96 ÷ 4)",
      answer: "176",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "105",
        "176",
        "26",
        "16"
      ]
    },
    {
      question: "Simplify : 3 + 6 x (5 + 4) ÷ 3 - 7",
      answer: "14",
      time: 15,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "11",
        "16",
        "14",
        "15"
      ]
    },
    {
      question: "Simplify :150 ÷ (6 + 3 x 8) - 5",
      answer: "0",
      time: 15,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "2",
        "5",
        "0",
        "None of these"
      ]
    },
  ]

  getQuestion() : question{
    if(this.currentQuestion >= this.questions.length){
      return {
        question: "",
        answer: "",
        time: 0,
        type: "text",
        finished: true,
      }
    }else{
      return this.questions[this.currentQuestion++];
    }
  }
}
