import { Component, OnInit } from '@angular/core';
import { WarehouseCell } from '../../../models/warehouse-cell';
import { Router, ActivatedRoute } from '@angular/router';
import { WarehouseService } from '../../../services/warehouse/warehouse.service';
import { ToastrService } from 'ngx-toastr';
import { WarehouseCellService } from '../../../services/warehouse-cell/warehouse-cell.service';
import { CellStatus } from './../../../models/cell-status';

@Component({
  selector: 'noc-warehouse-cell-form',
  templateUrl: './warehouse-cell-form.component.html',
  styleUrls: ['./warehouse-cell-form.component.css']
})
export class WarehouseCellFormComponent implements OnInit {
  public get cellStatus(): string {
    switch (this.warehouseCell.status) {
      case CellStatus.available:
        return "available";
      case CellStatus.full:
        return "full";
      case CellStatus.fault:
        return "fault";
      default:
        return '';
    }
  }
  public set cellStatus(status: string) {
    switch (status) {
      case 'available':
        this.warehouseCell.status = CellStatus.available;
        break;
      case 'full':
        this.warehouseCell.status = CellStatus.full;
        break;
      case 'fault':
        this.warehouseCell.status = CellStatus.fault;
        break;
    }
  }

  public warehouseCell: WarehouseCell = new WarehouseCell({ status: CellStatus.available });

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private warehouseCellService: WarehouseCellService,
    private toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.cellStatus = 'available';
    this.activatedRoute.queryParams
      .subscribe(params => {
        this.warehouseCell.warehouseId = params.id;
      });
  }

  public onSubmit() {
    this.warehouseCellService.add(this.warehouseCell)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加库位成功", "成功");
        } else {
          this.toastrService.error("添加库位失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigate(['../warehouse-cell-table'], {
      relativeTo: this.activatedRoute,
      queryParams: { "id": this.warehouseCell.warehouseId }
    });
  }
}
