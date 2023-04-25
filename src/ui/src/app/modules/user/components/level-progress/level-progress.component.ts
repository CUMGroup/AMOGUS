import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserStats } from 'src/app/core/interfaces/user-stats';

@Component({
  selector: 'app-level-progress',
  templateUrl: './level-progress.component.html',
  styleUrls: ['./level-progress.component.css']
})
export class LevelProgressComponent implements OnInit, OnDestroy {

  constructor() { }

  @Input()
  stats$: Observable<UserStats>;
  statsSubscription: Subscription;

  stats: UserStats;

  currentLevel: number;
  currentXp: number;
  xpToNext: number;
  xpPerc: number;

  ngOnInit(): void {
    //this.currentLevel = this.xpToLevel(this.stats.Level);
    this.statsSubscription = this.stats$.subscribe(e => {
      this.currentLevel = this.xpToLevelConverter(e.level);
      this.currentXp = e.level;
      this.xpToNext = this.levelToXpConverter(this.currentLevel + 1);
      this.xpPerc = Math.floor((this.currentXp / this.xpToNext) * 100);
      this.stats = e;
    });
  }

  ngOnDestroy(): void {
    this.statsSubscription?.unsubscribe();
  }


  xpToLevelConverter(xp: number): number {
    return Math.floor(Math.sqrt(0.1 * xp));
  }

  levelToXpConverter(level: number): number {
    return Math.ceil(Math.pow(level, 2) * 10);
  }

}
