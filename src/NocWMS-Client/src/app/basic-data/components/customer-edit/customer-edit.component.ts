import { Component, OnInit } from '@angular/core';
import { Gender } from '../../../models/gender';
import { Customer } from '../../../models/customer';
import { UserService } from '../../../services/user/user.service';
import { CustomerService } from '../../../services/customer/customer.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'noc-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css']
})
export class CustomerEditComponent implements OnInit {

  public get gender(): string {
    return this.customer.gender === Gender.male ? "male" : "female";
  }
  public set gender(gender: string) {
    this.customer.gender = gender === "male" ? Gender.male : Gender.female;
  }
  public canEdit: boolean;
  public customer: Customer;
  constructor(
    private userService: UserService,
    private customerService: CustomerService,
    private toastrService: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.customer = new Customer();
    this.canEdit = this.userService.isAdmin();
    this.activatedRoute.queryParams
      .subscribe(params => {
        let id = params.id;
        this.customerService.get(id)
          .subscribe(data => {
            if (data.isSuccess) {
              this.customer = data.data as Customer;
            } else {
              this.toastrService.info("无法得到该客户信息", "提示");
            }
          });
      });
  }

  public onSubmit() {
    this.customerService.update(this.customer)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("更新客户数据成功", "成功");
          this.customer = data.data as Customer;
        } else {
          this.toastrService.error("更新客户数据失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/customer-table');
  }
}
