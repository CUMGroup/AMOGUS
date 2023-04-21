import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserStats } from 'src/app/core/interfaces/user-stats';

@Component({
  selector: 'app-pie-graph',
  templateUrl: './pie-graph.component.html',
  styleUrls: ['./pie-graph.component.css']
})
export class PieGraphComponent implements OnInit, OnDestroy {

  @Input()
  stats$: Observable<UserStats>;
  statsSubscription: Subscription;

  piOptions: any;

  dataPi: any[];

  echartInstance: any;

  ngOnInit(): void {
    
    this.statsSubscription = this.stats$.subscribe(e => {
      this.dataPi = Object.keys(e.categorieAnswers)
          .map(ans => ({name: ans, value: e.categorieAnswers[ans]}));
      this.initPiChart();
    })
  }

  onChartInit(event) {
    this.echartInstance = event;
  }

  ngOnDestroy(): void {
    this.statsSubscription.unsubscribe();
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
