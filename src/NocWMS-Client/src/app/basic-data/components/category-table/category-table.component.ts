import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Category } from './../../../models/category';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../../services/category/category.service';
import { ToastrService } from 'ngx-toastr';
import { PageData } from '../../../models/page-data';
import { UserService } from '../../../services/user/user.service';

@Component({
  selector: 'noc-category-table',
  templateUrl: './category-table.component.html',
  styleUrls: ['./category-table.component.css']
})
export class CategoryTableComponent implements OnInit {
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public categories: Array<Category>;
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
    private categoryService: CategoryService,
    private modalService: NgbModal,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 5;
    this.getCategorys();
  }

  private getCategorys() {
    if (this.canQuery) {
      this.categoryService.searchCategorys(this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Category>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.categories = pageData.data;
            }
          }
        });
    } else {
      this.categoryService.getCategorys(this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Category>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.categories = pageData.data;
            }
          }
        });
    }
  }

  public newCategory(item?: {}) {
    this.router.navigate(['../category-form'], { relativeTo: this.activatedRoute });
  }

  public editCategory(item: Category) {
    this.router.navigate(['../category-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: item.id
      }
    });
  }

  open(item: Category) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.categoryService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.categories = this.categories.filter(x => x.id !== item.id);
                this.toastrService.success(`类别： ${item.categoryName}`, "删除成功");
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
    this.getCategorys();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getCategorys();
  }
}
