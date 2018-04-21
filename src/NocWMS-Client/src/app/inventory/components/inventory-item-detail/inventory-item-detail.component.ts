import { NgxEchartsDirective } from 'ngx-echarts';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbTabsetConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { InventoryService } from '../../../services/inventory/inventory.service';
import { ToastrService } from 'ngx-toastr';
import { Inventory } from '../../../models/inventory';
import { InventoryCell } from '../../../models/inventory-cell';

@Component({
  selector: 'noc-inventory-item-detail',
  templateUrl: './inventory-item-detail.component.html',
  styleUrls: ['./inventory-item-detail.component.css'],
  providers: [NgbTabsetConfig]
})
export class InventoryItemDetailComponent implements OnInit {

  public echartInstance;
  public options = {};
  public id: number;
  public inventory: Inventory;
  constructor(
    public config: NgbTabsetConfig,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private inventoryService: InventoryService) {
    config.justify = 'center';
    config.type = 'pills';
  }

  ngOnInit() {
    this.activatedRoute.queryParams
      .subscribe(params => {
        this.id = params.id;
        this.getInventory();
      });
  }

  private getInventory() {
    this.inventoryService.get(this.id)
      .subscribe(data => {
        if (data.isSuccess) {
          this.inventory = data.data as Inventory;
        }
      });
  }

  public backToList(): void {
    this.router.navigateByUrl('/workspace/inventory/inventory-table');
  }

  public printBill() {
    (<any>window).print();
  }

  public onChartInit(ec) {
    this.echartInstance = ec;
    this.options = {
      title: {
        text: '库存变化曲线',
        subtext: '纯属虚构',
        x: "center"
      },
      tooltip: {
        trigger: 'axis'
      },
      xAxis: {
        type: 'category',
        boundaryGap: false,
        data: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']
      },
      yAxis: {
        type: 'value',
        axisLabel: {
          formatter: '{value} 个'
        }
      },
      series: [
        {
          name: '入库',
          type: 'line',
          data: [11, 11, 15, 13, 12, 13, 10, 123, 100, 99, 66, 199]
        },
        {
          name: '出库',
          type: 'line',
          data: [110, 110, 150, 130, 120, 130, 100, 1230, 1000, 990, 660, 1990]
        }
      ]
    };
    setTimeout(() => {
      this.echartInstance.setOption(this.options);
    }, 500);
  }
}
