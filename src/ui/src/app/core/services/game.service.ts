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
      question: "Find x : x + 10 = 0",
      answer: "-10",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "2",
        "-5",
        "-10",
        "8"
      ]
    },
    {
      question: "Find x : x^2 - 2 = 2 ",
      answer: "2",
      time: 15,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "2",
        "3",
        "0",
        "4"
      ]
    },
    {
      question: "Find x : 5x - 11 = 3x + 9",
      answer: "10",
      time: 20,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "10",
        "13",
        "4",
        "None of these"
      ]
    },
    {
      question: "Find x : 9 - 2(x - 5) = x + 10",
      answer: "3",
      time: 25,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "2",
        "4",
        "5",
        "3"
      ]
    },
    {
      question: "Simplify : 3/4 + 1/4",
      answer: "1",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "1",
        "2",
        "3",
        "4"
      ]
    },
    {
      question: "Simplify : (1/8)^2 + 7/16 ",
      answer: "0.5",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "0.25",
        "0.5",
        "0.75",
        "None of these"
      ]
    },
    {
      question: "Simplify : 4/2 - 1/2 + 2",
      answer: "3.5",
      time: 10,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "1",
        "2.5",
        "2",
        "3.5"
      ]
    },
    {
      question: "Solve : 2+2",
      answer: "4",
      time: 7,
      type: "multipleChoice",
      multipleChoiceAnswers: [
        "1",
        "2",
        "3",
        "4"
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
