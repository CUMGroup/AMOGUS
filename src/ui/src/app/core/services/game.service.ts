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

  sessionStartTime : number;

  session : GameSession;
  constructor(private apiService : ApiService) {
  }

  questions: question[];

  startNewGame(category : CategoryType) : Observable<GameSession> {
    return this.apiService.post<GameSession>('/game/new', category)
      .pipe(tap(
        e => {
          this.questions = e.questions.map(
            quest => new question(quest.answer, quest.category, quest.difficulty, quest.exercise, quest.experiencePoints, quest.help, quest.questionId, quest.wrongAnswers, false)
          );
          this.session = new GameSession(e.sessionId, e.userId, 0, e.correctAnswersCount, e.givenAnswersCount, 0, Math.min(), 0, e.category, this.questions);
          this.currentQuestion = 0;
          this.loading = false;
          this.sessionStartTime = new Date().getTime();
        }
      ));
  }

  endGame() : Observable<unknown> {
    this.session.playTime = new Date().getTime() - this.sessionStartTime;
    return this.apiService.post('/game/end', this.session);
  }

  getQuestion() : question{
    if(this.loading)
      return null;
    if(this.currentQuestion >= this.questions.length){
      return question.finished();
    }else{
      return this.questions[this.currentQuestion++];
    }
  }

  getSession() : GameSession {
    return this.session;
  }
}
