import { Component, Input, OnInit } from '@angular/core';
import { NgxEchartsModule } from 'ngx-echarts';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { DataSet } from '../interfaces/DataSet';

const CONFIG_DEFAULT = {
  isInsideGrid: false,
  isTooltipShown: false,
  isLabelShown: true,
};

@Component({
  selector: 'app-graphs',
  templateUrl: './graphs.component.html',
  styleUrls: ['./graphs.component.css']
})
export class GraphsComponent implements OnInit {

  @Input() dataSet: Array<DataSet>

  constructor() { }

  defaultData = [
    {
      name: 'Afghanistan',
      value: 44
    },
    {
      name: 'Albania',
      value: 17
    },
    {
      name: 'Algeria',
      value: 24
    },
    {
      name: 'Germany',
      value: 35
    }
  ]

  options: any;

  graphOptions: any;

  graph2Options: any;

  xAxisData = [];
  data1 = [];
  data2 = [];
  data3 = [];

  showChart = true;

  config = CONFIG_DEFAULT;

  echartInstance: any;

  ngOnInit(): void {
    for (let i = 0; i < 100; i++) {
      this.xAxisData.push('category' + i);
      this.data1.push((Math.sin(i / 5) * (i / 5 - 10) + i / 6) * 5);
      this.data2.push((Math.cos(i / 5) * (i / 5 - 10) + i / 6) * 5);
      this.data3.push(this.data1[i] / 10 * this.data2[i] / 10);
    }

    this.graph2Options = {
      legend: {
        data: ['bar', 'bar2', 'bar3'],
        align: 'left',
      },
      tooltip: {},
      xAxis: {
        data: this.xAxisData,
        silent: false,
        splitLine: {
          show: false,
        },
      },
      yAxis: {},
      series: [
        {
          name: 'bar',
          type: 'bar',
          data: this.data1,
          animationDelay: (idx) => idx * 10,
        },
        {
          name: 'bar2',
          type: 'bar',
          data: this.data2,
          animationDelay: (idx) => idx * 10 + 100,
        },
        {
          name: 'bar3',
          type: 'bar',
          data: this.data3,
          animationDelay: (idx) => idx * 10 + 200,
        },
      ],
      animationEasing: 'elasticOut',
      animationDelayUpdate: (idx) => idx * 5,
    };

    this.graphOptions = {
      xAxis: {
        type: 'category',
        boundaryGap: false,
        data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
      },
      yAxis: {
        type: 'value'
      },
      series: [{
        data: [820, 932, 901, 934, 1290, 1430, 1550, 1200, 1650, 1450, 1680, 1890],
        type: 'line',
        areaStyle: {}
      }]
    }

    this.options = {
      tooltip: {
        show: this.config.isTooltipShown,
        trigger: 'item',
        formatter: '{a} <br/>{b} : {c} ({d}%'
      },
      series: [
        {
          name: '',
          type: 'pie',
          radius: '70%',
          data: this.data(),
          top: 0,
          bottom: 0,
          left: 0,
          right: 0,
          itemStyle: {
            borderRadius: 10,
            borderColor: "#fff",
            borderWidth: 2,
            emphasis: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
          },
          label: {
            show: true,
            alignTo: 'none'
          }
        }
      ]
    };
    this.setChartConfig();
  }

  setChartConfig() {
    let firstSeries = <any>this.options.series[0];

    firstSeries.data = this.data();

    if (this.config.isInsideGrid) {
      firstSeries.top = '100';
      firstSeries.bottom = '100';
    } else {
      firstSeries.top = '0';
      firstSeries.bottom = '0';
    }

    this.options.tooltip.show = this.config.isTooltipShown;

    firstSeries.label.show = this.config.isLabelShown;
  }

  changeData() {
    this.defaultData = [
      {
        name: 'some',
        value: 12
      },
      {
        name: 'new',
        value: 11
      },
      {
        name: 'data',
        value: 4
      },
      {
        name: 'I',
        value: 10
      },
      {
        name: 'guess',
        value: 10
      }
    ]
    this.setChartConfig()
  }

  onChartInit(event) {
    this.echartInstance = event;
  }
  onConfigChange() {
    this.setChartConfig();
    this.showChart = false;
    setTimeout(() => {
      this.showChart = true;
    }, 10);
  }

  fixIt() {
    Object.assign(this.config, {
      isInsideGrid: false,
      isTooltipShown: true,
      isLabelShown: false,
      aLotOfValues: true
    });
    this.onConfigChange();
  }

  data() {
    if (this.dataSet) {
      return this.dataSet
    } else {
      return this.defaultData;
    }
  }
}
