import { Component, Input, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserStats } from 'src/app/core/interfaces/user-stats';

@Component({
  selector: 'app-line-graph',
  templateUrl: './line-graph.component.html',
  styleUrls: ['./line-graph.component.css']
})
export class LineGraphComponent implements OnInit {

  constructor() { }

  @Input()
  stats$: Observable<UserStats>;
  statsSubscription: Subscription;

  lineOptions: any;

  dataLine: any[];

  echartInstance: any;

  ngOnInit(): void {

    this.statsSubscription = this.stats$.subscribe(e => {
      this.dataLine = e.CorrectPerDay
    })
    this.initLineChart();

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
        data: this.last5DaysAsStringArr(date)
      },
      yAxis: {
        type: 'value'
      },
      series: [{
        data: this.dataLine,
        type: 'line',
        areaStyle: {}
      }]
    }
  }

  last5DaysAsStringArr(date: Date): string[] {
    const dates = [...Array(5)].map((_, i) => {
      const d = new Date(date);
      d.setDate(d.getDate() - i)
      return d.getDay() + "." + d.getMonth() + ".";
    })

    return dates;
  }

}
