import { Component, Input, OnInit } from '@angular/core';
import { UserStats } from 'src/app/core/interfaces/user-stats';

@Component({
  selector: 'app-stats-table',
  templateUrl: './stats-table.component.html',
  styleUrls: ['./stats-table.component.css']
})
export class StatsTableComponent implements OnInit {

  constructor() { }

  @Input()
  stats: UserStats;

  ngOnInit(): void {
  }

}
