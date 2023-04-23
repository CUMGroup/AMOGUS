import { Injectable } from '@angular/core';
import { question } from "../interfaces/question";
import { Observable, tap } from 'rxjs';
import { CategoryType, GameSession } from '../interfaces/game-session';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  public loading : boolean = true;

  currentQuestion = 0;
  session : GameSession;
  constructor(private apiService : ApiService) { }

  startNewGame(category : CategoryType) : Observable<GameSession> {
    return this.apiService.post<GameSession>('/game/new', {category: category})
          .pipe(tap(
            e => {
              this.session = e;
              this.questions = e.questions;
              this.currentQuestion = 0;
              this.loading = false;

              }
          ));
  }

  endGame() : Observable<unknown> {
    return this.apiService.post('/game/end', this.session);
  }
    // mock data
  questions: question[];

  getQuestion() : question{
    if(this.loading)
      return null;

    if(this.currentQuestion >= this.questions.length){
      return {
        answer: "",
        wrongAnswers: [""],
        help:"",
        difficultyType:"",
        categoryType: "",
        multipleChoiceAnswers: [
        ],
        exercise: "",
        time: 0,
        finished: true,
      }
    }else{
      return this.questions[this.currentQuestion++];
    }
  }
}
