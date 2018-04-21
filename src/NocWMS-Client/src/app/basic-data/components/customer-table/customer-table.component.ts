import { UserService } from './../../../services/user/user.service';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Customer } from './../../../models/customer';
import { CustomerService } from '../../../services/customer/customer.service';
import { PageData } from '../../../models/page-data';

@Component({
  selector: 'app-customer-table',
  templateUrl: './customer-table.component.html',
  styleUrls: ['./customer-table.component.css']
})
export class CustomerTableComponent implements OnInit {
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public customers: Array<Customer>;
  public total: number;
  public page: number;
  private _pageSize: number;
  public get pageSize() {
    return this._pageSize;
  }
  public set pageSize(pageSize) {
    this._pageSize = pageSize;
    this.pageChange(this.page);
  }
  public readonly pageSizes = [5, 10, 20];


  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private customerService: CustomerService,
    private modalService: NgbModal,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 5;
    this.getCustomers();
  }

  private getCustomers() {
    if (this.canQuery) {
      this.customerService.searchCustomers(this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Customer>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.customers = pageData.data;
            }
          }
        });
    } else {
      this.customerService.getCustomers(this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Customer>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.customers = pageData.data;
            }
          }
        });
    }
  }

  public newCustomer(item?: {}) {
    this.router.navigate(['../customer-form'], { relativeTo: this.activatedRoute });//.navigateByUrl('/workspace/basic-data/customer-form',);
  }

  public editCustomer(item: Customer) {
    // this.router.navigate(['../customer-edit', item.id], {
    //   relativeTo: this.activatedRoute,
    // });
    this.router.navigate(['../customer-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: item.id
      }
    });
  }

  open(item: Customer) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.customerService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.customers = this.customers.filter(x => x.id !== item.id);
                this.toastrService.success(`客户： ${item.customerName}`, "删除成功");
              } else {
                this.toastrService.error(data.message, "错误");
              }
            });
        } else {
          this.toastrService.info("您的权限不足");
        }
      }
    });
  }

  public close(info: string | null) {
    if (info) {
      this.modalRef.close(info);
    } else {
      this.modalRef.dismiss();
    }
  }

  public pageChange(page: number) {
    this.page = page;
    this.getCustomers();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getCustomers();
  }
}
