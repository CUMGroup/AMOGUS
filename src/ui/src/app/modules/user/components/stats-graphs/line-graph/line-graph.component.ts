import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserStats } from 'src/app/core/interfaces/user-stats';

@Component({
  selector: 'app-line-graph',
  templateUrl: './line-graph.component.html',
  styleUrls: ['./line-graph.component.css']
})
export class LineGraphComponent implements OnInit, OnDestroy {

  constructor() { }

  @Input()
  stats$: Observable<UserStats>;
  statsSubscription: Subscription;

  lineOptions: any;

  dataLine: Map<Date, number>;

  echartInstance: any;

  ngOnInit(): void {

    this.statsSubscription = this.stats$.subscribe(e => {
      this.dataLine = e.correctAnswersPerDay;
      this.initLineChart();
    })

  }

  onChartInit(event) {
    this.echartInstance = event;
  }

  ngOnDestroy(): void {
    this.statsSubscription.unsubscribe();
  }

  initLineChart(): void {
    const date = new Date();
    this.lineOptions = {
      title: {
        text: 'Correct Answers per Day',
        textStyle: {
          color: '#eeeeee'
        }
      },
      xAxis: {
        type: 'category',
        boundaryGap: false,
        data: this.sortCorrectAnswersPerDayArray(this.dataLine).map(e => new Date(e[0])).map(d => d.getDate() + '.' + (d.getMonth() + 1))
      },
      yAxis: {
        type: 'value'
      },
      series: [{
        data: this.sortCorrectAnswersPerDayArray(this.dataLine).map(e => e[1]),
        type: 'line',
        areaStyle: {}
      }]
    }
  }

  sortCorrectAnswersPerDayArray(data) : [string, any][] {
    return Object.entries(data).sort((a,b) => new Date(a[0]).getTime() - new Date(b[0]).getTime());
  }

}
