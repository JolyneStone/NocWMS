import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Product } from '../../../models/product';
import { PageData } from '../../../models/page-data';
import { ProductService } from '../../../services/product/product.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../services/user/user.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'noc-product-table',
  templateUrl: './product-table.component.html',
  styleUrls: ['./product-table.component.css']
})
export class ProductTableComponent implements OnInit {
  public id: number;
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public products: Array<Product>;
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
    private productCellService: ProductService,
    private modalService: NgbModal,
    private toastrService: ToastrService
  ) {
    this.activatedRoute.queryParams
      .subscribe(params => {
        this.id = params.id;
      });
  }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 5;
    this.getProducts();
  }

  private getProducts() {
    if (this.canQuery) {
      this.productCellService.searchProducts(this.id, this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Product>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.products = pageData.data;
            }
          }
        });
    } else {
      this.productCellService.getProducts(this.id, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Product>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.products = pageData.data;
            }
          }
        });
    }
  }

  public newProduct() {
    this.router.navigate(['../product-form'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: this.id
      }
    });
  }

  public editProduct(item: Product) {
    this.router.navigate(['../product-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: this.id,
        productId: item.id
      }
    });
  }

  open(item: Product) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.productCellService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.products = this.products.filter(x => x.id !== item.id);
                this.toastrService.success(`产品： ${item.productName}`, "删除成功");
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
    this.getProducts();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getProducts();
  }
}
