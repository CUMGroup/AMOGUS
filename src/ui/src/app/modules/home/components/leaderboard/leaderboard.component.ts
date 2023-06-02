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
  streakColumnsToDisplayWithBadge = ['place', 'username', 'streak', 'badge'];
  
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

  daysToYearConverter(days: number): string {
    if(days < 365) return days + "d";

    const years = Math.floor(days / 365);
    const spareDays = (days % 365);

    if(spareDays == 0) return years + "y"

    return years + "y " + spareDays + "d";
  }

  badgeCalculator(longestStreak: number): string {
    if(longestStreak <= 0)return ""
    if(longestStreak <= 6) return "assets/Badges/opt/first_day_badge.webp";
    if(longestStreak <= 29) return "assets/Badges/opt/7_days_badge.webp";
    if(longestStreak <= 179) return "assets/Badges/opt/month_badge.webp";
    if(longestStreak <= 364) return "assets/Badges/opt/half_year_badge.webp";
    if(longestStreak <= 729) return "assets/Badges/opt/1_year_badge.webp";
    if(longestStreak <= 1824) return "assets/Badges/opt/2_years_badge.webp";
    if(longestStreak <= 3649) return "assets/Badges/opt/5_years_badge.webp";
    if(longestStreak <= 9124) return "assets/Badges/opt/10_years_badge.webp";
    if(longestStreak <= 15249) return "assets/Badges/opt/25_years_badge.webp";
    if(longestStreak <= 27375) return "assets/Badges/opt/50_years_badge.webp";
    if(longestStreak <= 36499) return "assets/Badges/opt/75_years_badge.webp";
    return "assets/Badges/opt/100_years_badge.webp";
  }

  toolTipCalculator(longestStreak: number): string {
    if(longestStreak <= 0)return ""
    if(longestStreak <= 6) return "It's your first day, yay!";
    if(longestStreak <= 29) return "What a hot week!";
    if(longestStreak <= 179) return "A whole month so far!";
    if(longestStreak <= 364) return "Half a year!";
    if(longestStreak <= 729) return "We've come full circle!";
    if(longestStreak <= 1824) return "Double year!";
    if(longestStreak <= 3649) return "Penta-year, wow!";
    if(longestStreak <= 9124) return "A full decade now!";
    if(longestStreak <= 15249) return "Quarter century, dude!";
    if(longestStreak <= 27375) return "50 years, holy!";
    if(longestStreak <= 36499) return "Time to give your account to your grandchildren!";
    return "Maaan, you are doing maths from your graaave! 💀";
  }
}
