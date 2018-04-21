import { CategoryService } from './../../../services/category/category.service';
import { WarehouseService } from './../../../services/warehouse/warehouse.service';
import { Warehouse } from './../../../models/warehouse';
import { Category } from './../../../models/category';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { InventoryService } from '../../../services/inventory/inventory.service';
import { Inventory } from '../../../models/inventory';
import { PageData } from '../../../models/page-data';
import { UserService } from '../../../services/user/user.service';

@Component({
  selector: 'noc-inventory-table',
  templateUrl: './inventory-table.component.html',
  styleUrls: ['./inventory-table.component.css']
})
export class InventoryTableComponent implements OnInit {

  //仓库
  public warehouses: Array<Warehouse>;
  //品类
  public categories: Array<Category>;
  //商品
  public inventories: Array<Inventory>;
  public selectedWarehouse: number;
  public selectedCategory: number;
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
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
    private inventoryService: InventoryService,
    private warehouseService: WarehouseService,
    private categoryService: CategoryService,
    private modalService: NgbModal,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.selectedWarehouse = -1;
    this.selectedCategory = -1;
    this.page = 1;
    this.pageSize = 5;
    this.warehouseService.getSimples()
      .subscribe(data => {
        this.warehouses = data.isSuccess ? data.data as Warehouse[] : [];
      });
    this.categoryService.getSimples()
      .subscribe(data => {
        this.categories = data.isSuccess ? data.data as Category[] : [];
      });
  }

  private getInventories() {
    if (this.canQuery) {
      this.inventoryService.searchInventories(this.selectedWarehouse, this.selectedCategory, this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Inventory>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.inventories = pageData.data as Inventory[];
            }
          }
        });
    } else {
      this.inventoryService.getInventories(this.selectedWarehouse, this.selectedCategory, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Inventory>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.inventories = pageData.data;
            }
          }
        });
    }
  }

  public newInventory() {
    this.router.navigate(['../inventory-form'], { relativeTo: this.activatedRoute });
  }

  public editInventory(item: Inventory) {
    this.router.navigate(['../inventory-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: item.id
      }
    });
  }

  open(item: Inventory) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.inventoryService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.inventories = this.inventories.filter(x => x.id !== item.id);
                this.toastrService.success("库存删除成功", "删除成功");
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
    this.getInventories();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getInventories();
  }

  public setWarehouse(event) {
    console.log(event);
    this.selectedWarehouse = event;
    this.page = 1;
    this.getInventories();
  }

  public setCategory(event) {
    this.selectedCategory = event;
    this.page = 1;
    this.getInventories();
  }

  public editItem(item: Inventory) {
    this.router.navigate(['../inventory-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: { id: item.id }
    })
  }

  public entryItem(item: Inventory) {
    this.router.navigate(['../inventory-item-detail'], {
      relativeTo: this.activatedRoute,
      queryParams: { id: item.id }
    });
  }
}
