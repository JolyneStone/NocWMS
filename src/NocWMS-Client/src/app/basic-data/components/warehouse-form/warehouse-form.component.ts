import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Warehouse } from '../../../models/warehouse';
import { WarehouseService } from '../../../services/warehouse/warehouse.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'noc-warehouse-form',
  templateUrl: './warehouse-form.component.html',
  styleUrls: ['./warehouse-form.component.css']
})
export class WarehouseFormComponent implements OnInit {
  public warehouse: Warehouse = new Warehouse();

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private warehouseService: WarehouseService,
    private toastrService: ToastrService) {
  }

  ngOnInit(): void {
  }

  public onSubmit() {
    this.warehouseService.add(this.warehouse)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加仓库成功", "成功");
        } else {
          this.toastrService.error("添加仓库失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/warehouse-table');
  }
}
