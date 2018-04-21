import { ToastrService } from 'ngx-toastr';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { VendorService } from '../../../services/vendor/vendor.service';
import { Vendor } from '../../../models/vendor';
import { PageData } from '../../../models/page-data';
import { UserService } from '../../../services/user/user.service';

@Component({
  selector: 'noc-vendor-table',
  templateUrl: './vendor-table.component.html',
  styleUrls: ['./vendor-table.component.css']
})
export class VendorTableComponent implements OnInit {
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public vendors: Array<Vendor>;
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
    private vendorService: VendorService,
    private modalService: NgbModal,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 5;
    this.getVendors();
  }

  private getVendors() {
    if (this.canQuery) {
      this.vendorService.searchVendors(this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Vendor>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.vendors = pageData.data;
            }
          }
        });
    } else {
      this.vendorService.getVendors(this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Vendor>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.vendors = pageData.data;
            }
          }
        });
    }
  }

  public newVendor() {
    this.router.navigate(['../vendor-form'], { relativeTo: this.activatedRoute });//.navigateByUrl('/workspace/basic-data/vendor-form',);
  }

  public editVendor(item: Vendor) {
    // this.router.navigate(['../vendor-edit', item.id], {
    //   relativeTo: this.activatedRoute,
    // });
    this.router.navigate(['../vendor-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: item.id
      }
    });
  }

  open(item: Vendor) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.vendorService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.vendors = this.vendors.filter(x => x.id !== item.id);
                this.toastrService.success(`客户： ${item.vendorName}`, "删除成功");
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
    this.getVendors();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getVendors();
  }
}
