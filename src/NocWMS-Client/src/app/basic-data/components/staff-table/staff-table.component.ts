import { UserService } from './../../../services/user/user.service';
import { ElementRef } from '@angular/core';
import { PageData } from './../../../models/page-data';
import { Staff } from './../../../models/staff';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { StaffService } from '../../../services/staff/staff.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'noc-staff-table',
  templateUrl: './staff-table.component.html',
  styleUrls: ['./staff-table.component.css']
})
export class StaffTableComponent implements OnInit {
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public staffs: Array<Staff>;
  public total: number;
  public page: number;
  private _pageSize: number;
  public get pageSize() {
    return this._pageSize;
  }
  public set pageSize(pageSize) {
    this._pageSize = pageSize;
    this.page = 1;
    this.getStaffs();
  }

  public pageSizes = [5, 10, 20];
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private staffService: StaffService,
    private modalService: NgbModal,
    private toastrService: ToastrService) {
  }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 5;
    this.getStaffs();
  }

  private getStaffs() {
    if (this.canQuery) {
      this.staffService.searchStaffs(this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Staff>;
            if (pageData) {
              this.total = pageData.total;
              this.staffs = pageData.data;
            }
          }
        });
    } else {
      this.staffService.getStaffs(this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            var pageData = data.data as PageData<Staff>;
            if (pageData) {
              this.total = pageData.total;
              this.staffs = pageData.data;
            }
          }
        });
    }
  }

  public newStaff(item?: {}) {
    this.router.navigate(['../staff-form'], { relativeTo: this.activatedRoute });//.navigateByUrl('/workspace/basic-data/staff-form',);
  }

  public editStaff(item: Staff) {
    // this.router.navigate(['../staff-edit', item.id], {
    //   relativeTo: this.activatedRoute,
    // });
    this.router.navigate(['../staff-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: item.id
      }
    });
  }

  open(item: Staff) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.staffService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.staffs = this.staffs.filter(x => x.id !== item.id);
                this.toastrService.success(`员工： ${item.staffName}`, "删除成功");
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
    this.getStaffs();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getStaffs();
  }
}
