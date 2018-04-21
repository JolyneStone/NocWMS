import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { WarehouseCell } from '../../../models/warehouse-cell';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../../services/user/user.service';
import { ToastrService } from 'ngx-toastr';
import { PageData } from '../../../models/page-data';
import { WarehouseCellService } from '../../../services/warehouse-cell/warehouse-cell.service';

@Component({
  selector: 'noc-warehouse-cell-table',
  templateUrl: './warehouse-cell-table.component.html',
  styleUrls: ['./warehouse-cell-table.component.css']
})
export class WarehouseCellTableComponent implements OnInit {
  public id: number;
  @ViewChild('query')
  private query: ElementRef;
  private queryString: string;
  private canQuery: boolean;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public warehouseCells: Array<WarehouseCell>;
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
    private warehouseCellCellService: WarehouseCellService,
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
    this.getWarehouseCells();
  }

  private getWarehouseCells() {
    if (this.canQuery) {
      this.warehouseCellCellService.searchWarehouseCells(this.id, this.queryString, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<WarehouseCell>;
            if (pageData) {
              this.total = pageData.total;
              this.warehouseCells = pageData.data;
            }
          }
        });
    } else {
      this.warehouseCellCellService.getWarehouseCells(this.id, this.page, this.pageSize)
        .subscribe(data => {
          if (data.isSuccess) {
            let pageData = data.data as PageData<WarehouseCell>;
            if (pageData) {
              this.total = pageData.total;
              this.warehouseCells = pageData.data;
            }
          }
        });
    }
  }

  public newWarehouseCell() {
    this.router.navigate(['../warehouse-cell-form'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: this.id
      }
    });
  }

  public editWarehouseCell(item: WarehouseCell) {
    this.router.navigate(['../warehouse-cell-edit'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: this.id,
        cellId: item.id
      }
    });
  }

  open(item: WarehouseCell) {
    this.modalRef = this.modalService.open(this.content);
    this.modalRef.result.then((result) => {
      if (result === 'delete') {
        if (this.userService.isAdmin()) {
          this.warehouseCellCellService.delete(item.id)
            .subscribe(data => {
              if (data.isSuccess) {
                this.warehouseCells = this.warehouseCells.filter(x => x.id !== item.id);
                this.toastrService.success(`库位： ${item.cellName}`, "删除成功");
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
    this.getWarehouseCells();
  }

  public queryResult() {
    this.queryString = this.query.nativeElement.value;
    this.page = 1;
    if (this.queryString) {
      this.canQuery = true;
    } else {
      this.canQuery = false;
    }

    this.getWarehouseCells();
  }
}
