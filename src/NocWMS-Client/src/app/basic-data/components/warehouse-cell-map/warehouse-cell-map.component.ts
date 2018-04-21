import { WarehouseCellService } from './../../../services/warehouse-cell/warehouse-cell.service';
import { WarehouseCell } from './../../../models/warehouse-cell';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'noc-warehouse-cell-map',
  templateUrl: './warehouse-cell-map.component.html',
  styleUrls: ['./warehouse-cell-map.component.css']
})
export class WarehouseCellMapComponent implements OnInit {

  private cells: WarehouseCell[] = [];
  private isShow: boolean = false;

  constructor(
    private warehouseCellService: WarehouseCellService
  ) { }

  ngOnInit() {
  }

  public show(id: number) {
    this.warehouseCellService.getCellMap(id)
      .subscribe(data => {
        if (data.isSuccess) {
          this.cells = data.data as WarehouseCell[];
          this.isShow = this.cells && this.cells.length > 0;
        } else {
          this.isShow = false;
        }
      })
  }
}
