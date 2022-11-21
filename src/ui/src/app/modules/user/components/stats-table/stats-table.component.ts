import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserStats } from 'src/app/core/interfaces/user-stats';

@Component({
  selector: 'app-stats-table',
  templateUrl: './stats-table.component.html',
  styleUrls: ['./stats-table.component.css']
})
export class StatsTableComponent implements OnInit, OnDestroy {

  constructor() { }

  @Input()
  stats$: Observable<UserStats>;
  statsSubscription: Subscription;

  stats: UserStats;
  currentLevel: number;
  totalTime: number;
  ciRatio: number;

  ngOnInit(): void {
    this.statsSubscription = this.stats$.subscribe(e => {
      this.stats = e;
      this.currentLevel = this.xpToLevelConverter(e.Level);
      this.ciRatio = e.CorrectAnswers / Math.max(e.OverallAnswered - e.CorrectAnswers, 1);
    })
  }

  ngOnDestroy(): void {
    this.statsSubscription.unsubscribe();
  }

  xpToLevelConverter(xp: number): number {
    return Math.floor(Math.sqrt(5 * xp));
  }

}
