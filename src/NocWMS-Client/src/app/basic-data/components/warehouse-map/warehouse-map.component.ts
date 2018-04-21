import { Province } from './../../../models/province';
import { Warehouse } from './../../../models/warehouse';
import { WarehouseService } from './../../../services/warehouse/warehouse.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-warehouse-map',
  templateUrl: './warehouse-map.component.html',
  styleUrls: ['./warehouse-map.component.css']
})
export class WarehouseMapComponent implements OnInit {
  public echartInstance;
  public provinces: Province[];
  public options = {};

  constructor(
    private warehouseService: WarehouseService
  ) { }

  ngOnInit() {
  }

  private setChartInit(ec) {
    this.echartInstance = ec;
    this.options = {
      title: [
        {
          textStyle: {
            color: "#000",
            fontSize: 18
          },
          subtext: "",
          text: "仓库分布图",
          top: "auto",
          subtextStyle: {
            color: "#333",
            fontSize: 12
          },
          left: "auto"
        }
      ],
      legend: [
        {
          selectedMode: "multiple",
          top: "top",
          orient: "horizontal",
          data: [
            ""
          ],
          left: "center",
          show: true
        }
      ],
      // tooltip: {
      //   //show: false //不显示提示标签
      //   formatter: '{b}', //提示标签格式
      //   backgroundColor: "#ff7f50",//提示标签背景颜色
      //   textStyle: { color: "#fff" } //提示标签字体颜色
      // },
      series: [{
        type: 'map',
        mapType: 'china',
        label: {
          // normal: {
          //   show: true,//显示省份标签
          //   textStyle: { color: "#c71585" }//省份标签字体颜色
          // },
          emphasis: {//对应的鼠标悬浮效果
            show: true,
            textStyle: { color: "#800080" }
          }
        },
        itemStyle: {
          normal: {
            borderWidth: .5,//区域边框宽度
            borderColor: '#009fe8',//区域边框颜色
            areaColor: "#ffefd5",//区域颜色
          },
          emphasis: {
            borderWidth: .5,
            borderColor: '#4b0082',
            areaColor: "#ffdead",
          }
        },
        data: this.provinces
      }]
    };
  }
  public onChartInit(ec) {
    this.warehouseService.getMap()
      .subscribe(data => {
        if (data.isSuccess) {
          const warehouses = data.data as Warehouse[];
          this.provinces = new Array<Province>();
          warehouses.forEach((warehouse) => this.provinces.push(
            new Province({ name: warehouse.province, selected: true })
          ));
          console.log(this.provinces);
          this.setChartInit(ec);
        }
      });
  }

  public onChartClick($event) {
    console.log($event);
  }
}
