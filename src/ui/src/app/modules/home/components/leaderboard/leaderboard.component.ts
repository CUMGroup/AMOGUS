import { Component, OnDestroy, OnInit } from '@angular/core';
import {  Subscription } from 'rxjs';
import { LeaderboardModel, LeaderboardUserCorrectRatio, LeaderboardUserStreak } from 'src/app/core/interfaces/leaderboard-model';
import { LeaderboardService } from 'src/app/core/services/leaderboard.service';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit, OnDestroy {

  constructor(private leaderboardService: LeaderboardService) { }
  
  leaderboards: LeaderboardModel;
  leaderboards$: Subscription;
  streakColumnsToDisplay = ['place', 'username', 'streak'];
  
  ngOnInit(): void {
    this.leaderboards$ = this.leaderboardService.getLeaderboards().subscribe(e => { this.leaderboards = new LeaderboardModel();
      this.leaderboards.longestStreaks = e.longestStreaks.map(ls => new LeaderboardUserStreak(ls.username, ls.streak));
      this.leaderboards.currentStreaks = e.currentStreaks.map(cs => new LeaderboardUserStreak(cs.username, cs.streak));
      this.leaderboards.correctRatios = e.correctRatios.map(cr => new LeaderboardUserCorrectRatio(cr.username, cr.correctRatio));
    });
  }
  
  ngOnDestroy(): void {
    this.leaderboards$?.unsubscribe();
  }
}
