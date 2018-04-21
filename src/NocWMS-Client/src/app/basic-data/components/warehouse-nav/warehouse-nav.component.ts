import { Warehouse } from './../../../models/warehouse';
import { Component, OnInit, Input } from '@angular/core';
import { WarehouseService } from '../../../services/warehouse/warehouse.service';

@Component({
  selector: 'noc-warehouse-nav',
  templateUrl: './warehouse-nav.component.html',
  styleUrls: ['./warehouse-nav.component.css']
})
export class WarehouseNavComponent implements OnInit {

  @Input()
  public id: number;
  public warehouse: Warehouse;
  constructor(
    private warehouseService: WarehouseService
  ) { }

  ngOnInit() {
    this.warehouseService.getNav(this.id)
      .subscribe(data => {
        if (data.isSuccess) {
          this.warehouse = data.data as Warehouse;
        }
      })
  }

}
