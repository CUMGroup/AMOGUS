import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserStats } from '../interfaces/user-stats';
import { TimeSpan } from '../types/time-span';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  // TODO: dummy Service - get data from backend

  constructor() { }

  getUserStats(): Observable<UserStats> {

    const stats: UserStats = {
      Level: 20,
      CurrentStreak: 3,

      OverallAnswered: 112,
      CorrectAnswers: 102,

      TotalTimePlayed: TimeSpan.fromHours(5.23),
      QuickestAnswer: TimeSpan.fromMilliseconds(732),
      SlowestAnswer: TimeSpan.fromSeconds(12),

      LongestStreak: 5,

      CategorieAnswers: new Map<string, number>([
        ["Analysis", 52],
        ["Algebra", 17],
        ["Arithmetic", 43]
      ]),
      CorrectPerDay: [
        53,
        70,
        85,
        99,
        102
      ],
      Unlocked: [
        "5: Unlocked Medium Difficulty",
        "10: Unlocked Daily Challenge",
        "20: Unlocked Advanced Difficulty"
      ],
      NextPrize: {
        level: 25,
        prize: "Key Pack (5 keys)"
      }
    };
    return of(stats);
  }
}
