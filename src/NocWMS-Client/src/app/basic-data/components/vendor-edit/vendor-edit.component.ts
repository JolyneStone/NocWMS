import { Component, OnInit } from '@angular/core';
import { Gender } from '../../../models/gender';
import { Vendor } from '../../../models/vendor';
import { UserService } from '../../../services/user/user.service';
import { VendorService } from '../../../services/vendor/vendor.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'noc-vendor-edit',
  templateUrl: './vendor-edit.component.html',
  styleUrls: ['./vendor-edit.component.css']
})
export class VendorEditComponent implements OnInit {
 public get gender(): string {
    return this.vendor.gender === Gender.male ? "male" : "female";
  }
  public set gender(gender: string) {
    this.vendor.gender = gender === "male" ? Gender.male : Gender.female;
  }
  public canEdit: boolean;
  public vendor: Vendor;
  constructor(
    private userService: UserService,
    private vendorService: VendorService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.vendor = new Vendor();
    this.canEdit = this.userService.isAdmin();
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.vendorService.get(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.vendor = data.data as Vendor;
            } else {
              this.toastrService.info("无法得到该供应商信息", "提示");
            }
          });
      });
  }

  public onSubmit() {
    this.vendorService.update(this.vendor)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新供应商数据成功", "成功");
          this.vendor = data.data as Vendor;
        } else {
          this.toastrService.error("更新供应商数据失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/vendor-table');
  }
}
