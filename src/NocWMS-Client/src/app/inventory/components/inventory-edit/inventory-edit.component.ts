import { Component, OnInit } from '@angular/core';
import { Inventory } from '../../../models/inventory';
import { UserService } from '../../../services/user/user.service';
import { InventoryService } from '../../../services/inventory/inventory.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'noc-inventory-edit',
  templateUrl: './inventory-edit.component.html',
  styleUrls: ['./inventory-edit.component.css']
})
export class InventoryEditComponent implements OnInit {

  public canEdit: boolean;
  public inventory: Inventory;
  constructor(
    private userService: UserService,
    private inventoryService: InventoryService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.inventory = new Inventory();
    this.canEdit = this.userService.isAdmin();
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.inventoryService.get(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.inventory = data.data as Inventory;
            } else {
              this.toastrService.info("无法得到该库存信息", "提示");
            }
          });
      });
  }

  public onSubmit() {
    if (this.canEdit) {
      this.inventoryService.update(this.inventory)
        .subscribe(data => {
          if (data.isSuccess) {
            this.toastrService.success("更新库存数据成功", "成功");
          } else {
            this.toastrService.error("更新库存数据失败: " + data.message, "失败");
          }
        });
    }
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/inventory/inventory-table');
  }
}
