import { Gender } from './../../../models/gender';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Vendor } from '../../../models/vendor';
import { VendorService } from '../../../services/vendor/vendor.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vendor-form',
  templateUrl: './vendor-form.component.html',
  styleUrls: ['./vendor-form.component.css']
})
export class VendorFormComponent implements OnInit {
  public get gender(): string {
    return this.vendor.gender === Gender.male ? "male" : "female";
  }
  public set gender(gender: string) {
    this.vendor.gender = gender === "male" ? Gender.male : Gender.female;
  }
  public vendor: Vendor = new Vendor({ gender: Gender.male });

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private vendorService: VendorService,
    private toastrService: ToastrService) {
    this.gender = "male";
  }

  ngOnInit(): void {
  }

  public onSubmit() {
    this.vendorService.add(this.vendor)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加供应商成功", "成功");
        } else {
          this.toastrService.error("添加供应商失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/vendor-table');
  }
}
