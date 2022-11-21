import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserStats } from 'src/app/core/interfaces/user-stats';
import { StatsService } from 'src/app/core/services/stats.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css']
})
export class StatsComponent implements OnInit {

  constructor(private statsService: StatsService) { }

  userStats$: Observable<UserStats>;

  ngOnInit(): void {
    this.userStats$ = this.statsService.getUserStats();
  }

}
