import { Component, OnInit } from '@angular/core';
import { WarehouseCell, CellStatus } from '../../../models/warehouse-cell';
import { UserService } from '../../../services/user/user.service';
import { WarehouseCellService } from '../../../services/warehouse-cell/warehouse-cell.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'noc-warehouse-cell-edit',
  templateUrl: './warehouse-cell-edit.component.html',
  styleUrls: ['./warehouse-cell-edit.component.css']
})
export class WarehouseCellEditComponent implements OnInit {
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

  public canEdit: boolean;
  public warehouseCell: WarehouseCell = new WarehouseCell();
  constructor(
    private userService: UserService,
    private warehouseCellService: WarehouseCellService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.canEdit = this.userService.isAdmin();
    this.activatedRoute.queryParams
      .subscribe(params => {
        this.warehouseCell.warehouseId = params.id;
        let cellId = params.cellId;
        this.warehouseCellService.get(cellId)
          .subscribe(data => {
            if (data.isSuccess) {
              this.warehouseCell = data.data as WarehouseCell;
            } else {
              this.toastrService.info("无法得到该库位信息", "提示");
            }
          });
      });
  }

  public onSubmit() {
    this.warehouseCellService.update(this.warehouseCell)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新库位数据成功", "成功");
          this.warehouseCell = data.data as WarehouseCell;
        } else {
          this.toastrService.error("更新库位数据失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigate(['../warehouse-cell-table'], {
      relativeTo: this.activatedRoute, queryParams: {
        'id': this.warehouseCell.warehouseId
      }
    });
  }
}
