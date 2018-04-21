import { WarehouseCell } from './../../../models/warehouse-cell';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Warehouse } from '../../../models/warehouse';
import { Router, ActivatedRoute } from '@angular/router';
import { WarehouseService } from '../../../services/warehouse/warehouse.service';
import { UserService } from '../../../services/user/user.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { PageData } from '../../../models/page-data';
import { WarehouseCellMapComponent } from '../warehouse-cell-map/warehouse-cell-map.component';

@Component({
  selector: 'noc-warehouse-table',
  templateUrl: './warehouse-table.component.html',
  styleUrls: ['./warehouse-table.component.css']
})
export class WarehouseTableComponent implements OnInit {
  @ViewChild(WarehouseCellMapComponent)
  private cellMap: WarehouseCellMapComponent;
  public cells: WarehouseCell[];
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public warehouses: Array<Warehouse>;
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
    private warehouseService: WarehouseService,
    private modalService: NgbModal,
    private toastrService: ToastrService
  ) {
  }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 5;
    this.getWarehouses();
  }

  private getWarehouses() {
    if (this.canQuery) {
      this.warehouseService.searchWarehouses(this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Warehouse>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.warehouses = pageData.data;
            }
          }
        });
    } else {
      this.warehouseService.getWarehouses(this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<Warehouse>;
            if (pageData) {
              this.total = pageData.total;
              this.page = 1;
              this.warehouses = pageData.data;
            }
          }
        });
    }
  }

  public newWarehouse() {
    this.router.navigate(['../warehouse-form'], { relativeTo: this.activatedRoute });
  }

  public editWarehouse(item: Warehouse) {
    this.router.navigate(['../warehouse-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: item.id
      }
    });
  }

  open(item: Warehouse) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.warehouseService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.warehouses = this.warehouses.filter(x => x.id !== item.id);
                this.toastrService.success(`客户： ${item.warehouseName}`, "删除成功");
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
    this.getWarehouses();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getWarehouses();
  }

  public showCells(item: Warehouse) {
    this.cellMap.show(item.id);
  }
}
