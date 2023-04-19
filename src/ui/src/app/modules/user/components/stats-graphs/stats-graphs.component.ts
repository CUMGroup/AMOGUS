import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscribable, Subscription } from 'rxjs';
import { UserStats } from 'src/app/core/interfaces/user-stats';

@Component({
  selector: 'app-stats-graphs',
  templateUrl: './stats-graphs.component.html',
  styleUrls: ['./stats-graphs.component.css']
})
export class StatsGraphsComponent implements OnInit, OnDestroy {

  constructor() { }

  @Input()
  stats$: Observable<UserStats>;
  statsSubscription: Subscription;

  piOptions: any;
  lineOptions: any;

  dataPi: any[];
  dataLine: Map<Date, number>;

  echartInstance: any;

  ngOnInit(): void {
    
    this.statsSubscription = this.stats$.subscribe(e => {
      this.dataPi = Object.keys(e.categorieAnswers).map(ans => ({name: ans, value: e.categorieAnswers[ans]}));
      this.initPiChart();
      this.dataLine = e.correctAnswersPerDay;
      console.log();
      this.initLineChart();
    })

    //this.initPiChart();
    //this.initLineChart();

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

  last5DaysAsStringArr(date: Date): string[] {
    const dates = [...Array(5)].map((_, i) => {
      const d = new Date(date);
      d.setDate(d.getDate() - i)
      return d.getDay() + "." + d.getMonth() + ".";
    })

    return dates;
  }

  initPiChart(): void {
    this.piOptions = {
      tooltip: {
        show: true,
        trigger: 'item',
        formatter: '{b} : {c} Questions answered ({d}%)'
      },
      title: {
        text: 'Answered Questions per Category',
        textStyle: {
          color: '#eeeeee'
        }
      },
      darkMode: true,
      series: [
        {
          name: '',
          type: 'pie',
          radius: '70%',
          data: this.dataPi,
          top: 0,
          bottom: 0,
          left: 0,
          right: 0,
          itemStyle: {
            borderRadius: 10,
            borderColor: "#fff",
            borderWidth: 1,
            emphasis: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
          },
          label: {
            show: true,
            alignTo: 'none',

          }
        }
      ]
    };
  }

}
