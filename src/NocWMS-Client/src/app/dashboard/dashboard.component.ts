import { OnDestroy } from '@angular/core';
import { Component, OnInit, Input, ViewChildren, QueryList, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { NgxEchartsDirective } from 'ngx-echarts';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'noc-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {
  lineOptions: any;
  updateLineOptions: any;
  lineTimer: any;
  private oneDay = 24 * 3600 * 1000;
  private now: Date;
  private lineValue: number;
  private lineData: any[];


  updateBarOptions: any;
  barTimer: any;
  loading: boolean;

  // @ViewChildren(NgxEchartsDirective)
  // public echarts: QueryList<NgxEchartsDirective>;

  public barOptions = {
    title: {
      text: '库存金额变化',
      subtext: '纯属虚构',
      x: 'center'
    },
    color: ['#3398DB'],
    tooltip: {
      trigger: 'axis',
      axisPointer: {            // 坐标轴指示器，坐标轴触发有效
        type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
      },
      formatter: "{b}月{a}:{c}"
    },
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      containLabel: true
    },
    xAxis: [
      {
        type: 'category',
        data: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        axisTick: {
          alignWithLabel: true
        }
      }
    ],
    yAxis: [
      {
        type: 'value'
      }
    ],
    series: [
      {
        name: '访问量',
        type: 'bar',
        barWidth: '60%',
        data: [10, 52, 200, 334, 390, 330, 220, 1000, 500, 444, 999, 11]
      }
    ]
  };

  // public lineOptions = {
  //   title: {
  //     text: '库存量变化',
  //     subtext: '纯属虚构',
  //     x: "center"
  //   },
  //   tooltip: {
  //     trigger: 'axis'
  //   },
  //   xAxis: {
  //     type: 'category',
  //     boundaryGap: false,
  //     data: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']
  //   },
  //   yAxis: {
  //     type: 'value',
  //     axisLabel: {
  //       formatter: '{value} 次'
  //     }
  //   },
  //   series: [
  //     {
  //       name: '访问量',
  //       type: 'line',
  //       data: [11, 11, 15, 13, 12, 13, 10, 123, 100, 99, 66, 199]
  //     }
  //   ]
  // };
  constructor() {
  }

  lineInit(ec) {
    // charInit 传递的就是初始化生成的图表实例
    console.log(ec.getHeight());
  }

  barInit(ec) {
    // charInit 传递的就是初始化生成的图表实例
    console.log(ec.getHeight());

  }

  lineClick(ec) {
    console.log(ec);
  }

  barClick(ec) {
    console.log(ec);
  }

  ngOnInit() {
    //this.lineTimer = setInterval(() => {
    //   for (let i = 0; i < 10; i++) {
    //     this.lineChart.series[0].data.shift();
    //     this.lineChart.series[0].data.push(Math.random() * 200);
    //   }

    // 方法1
    // update series data:
    // this.updateOptions = {
    //   series: [{
    //     data: this.lineChart.series[0].data
    //   }]
    // };
    // 方法2
    //   this.echarts.first.setOption(this.lineChart);
    // }, 2000);

    this.lineData = [];
    this.now = new Date(1997, 9, 3);
    this.lineValue = Math.random() * 1000;

    for (let i = 0; i < 1000; i++) {
      this.lineData.push(this.randomData());
    }

    // initialize chart options:
    this.lineOptions = {
      title: {
        text: '历史库存数据',
        subtext: '纯属虚构',
        x: 'center'
      },
      tooltip: {
        trigger: 'axis',
        formatter: (params) => {
          params = params[0];
          let date = new Date(params.name);
          return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear() + ' : ' + params.value[1];
        },
        axisPointer: {
          animation: false
        }
      },
      xAxis: {
        type: 'time',
        splitLine: {
          show: false
        }
      },
      yAxis: {
        type: 'value',
        boundaryGap: [0, '100%'],
        splitLine: {
          show: false
        }
      },
      series: [{
        name: 'Mocking Data',
        type: 'line',
        showSymbol: false,
        hoverAnimation: false,
        data: this.lineData
      }]
    };

    // Mock dynamic data:
    this.lineTimer = setInterval(() => {
      for (let i = 0; i < 5; i++) {
        this.lineData.shift();
        this.lineData.push(this.randomData());
      }

      // update series data:
      this.updateLineOptions = {
        series: [{
          data: this.lineData
        }]
      };
    }, 1000);
  }

  ngOnDestroy() {
    clearInterval(this.lineTimer);
  }

  randomData() {
    this.now = new Date(this.now.getTime() + this.oneDay);
    this.lineValue = this.lineValue + Math.random() * 21 - 10;
    return {
      name: this.now.toString(),
      value: [
        [this.now.getFullYear(), this.now.getMonth() + 1, this.now.getDate()].join('/'),
        Math.round(this.lineValue)
      ]
    };
  }
}
