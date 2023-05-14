import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription, range } from 'rxjs';
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

  badge: string;
  toolTip: string;
  longestStreak: string;
  currentStreak: string;

  stats: UserStats;
  currentLevel: number;
  totalTime: number;
  ciRatio: number;
  slowestAnswerInSec: number;

  ngOnInit(): void {
    this.statsSubscription = this.stats$.subscribe(e => {
      this.stats = e;

      this.badge = this.badgeCalculator(e.longestStreak);
      this.toolTip = this.toolTipCalculator(e.longestStreak);

      this.longestStreak = this.daysToYearConverter(e.longestStreak);
      this.currentStreak = this.daysToYearConverter(e.currentStreak);
      this.currentLevel = this.xpToLevelConverter(e.level);

      this.ciRatio = e.correctAnswers / Math.max(e.overallAnswered - e.correctAnswers, 1);
      this.slowestAnswerInSec = Math.floor(e.slowestAnswer / 1000);
    })
  }

  ngOnDestroy(): void {
    this.statsSubscription.unsubscribe();
  }

  xpToLevelConverter(xp: number): number {
    return Math.floor(Math.sqrt(5 * xp));
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
    if(longestStreak <= 6) return "assets/Badges/first_day_badge.PNG";
    if(longestStreak <= 29) return "assets/Badges/7_days_badge.PNG";
    if(longestStreak <= 179) return "assets/Badges/month_badge.PNG";
    if(longestStreak <= 364) return "assets/Badges/half_year_badge.PNG";
    if(longestStreak <= 729) return "assets/Badges/1_year_badge.PNG";
    if(longestStreak <= 1824) return "assets/Badges/2_years_badge.PNG";
    if(longestStreak <= 3649) return "assets/Badges/5_years_badge.PNG";
    if(longestStreak <= 9124) return "assets/Badges/10_years_badge.PNG";
    if(longestStreak <= 15249) return "assets/Badges/25_years_badge.PNG";
    if(longestStreak <= 27375) return "assets/Badges/50_years_badge.PNG";
    if(longestStreak <= 36499) return "assets/Badges/75_years_badge.PNG";
    return "assets/Badges/100_years_badge.PNG";
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
