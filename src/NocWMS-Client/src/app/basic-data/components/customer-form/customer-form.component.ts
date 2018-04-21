import { CustomerService } from './../../../services/customer/customer.service';
import { Gender } from './../../../models/gender';
import { Component, OnInit } from '@angular/core';
import { Customer } from '../../../models/customer';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'noc-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})
export class CustomerFormComponent implements OnInit {

  public get gender(): string {
    return this.customer.gender === Gender.male ? "male" : "female";
  }
  public set gender(gender: string) {
    this.customer.gender = gender === "male" ? Gender.male : Gender.female;
  }
  public customer: Customer = new Customer({ gender: Gender.male });

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private customerService: CustomerService,
    private toastrService: ToastrService) {
    this.gender = "male";
  }

  ngOnInit(): void {
  }

  public onSubmit() {
    this.customerService.add(this.customer)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加客户成功", "成功");
        } else {
          this.toastrService.error("添加客户失败: " + data.message, "失败");
        }
      });
  }

  public backToTable() {
    this.router.navigateByUrl('/workspace/basic-data/customer-table');
  }
}
