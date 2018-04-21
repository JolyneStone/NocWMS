import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user/user.service';
import { Warehouse } from '../../../models/warehouse';
import { WarehouseService } from '../../../services/warehouse/warehouse.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'noc-warehouse-edit',
  templateUrl: './warehouse-edit.component.html',
  styleUrls: ['./warehouse-edit.component.css']
})
export class WarehouseEditComponent implements OnInit {
  public canEdit: boolean;
  public warehouse: Warehouse = new Warehouse();
  constructor(
    private userService: UserService,
    private warehouseService: WarehouseService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.canEdit = this.userService.isAdmin();
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.warehouseService.get(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.warehouse = data.data as Warehouse;
            } else {
              this.toastrService.info("无法得到该仓库信息", "提示");
            }
          });
      });
  }

  public onSubmit() {
    this.warehouseService.update(this.warehouse)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新仓库数据成功", "成功");
          this.warehouse = data.data as Warehouse;
        } else {
          this.toastrService.error("更新仓库数据失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/warehouse-table');
  }
}
