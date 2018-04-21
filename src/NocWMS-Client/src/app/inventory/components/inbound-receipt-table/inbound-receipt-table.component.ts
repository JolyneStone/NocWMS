import { PageData } from './../../../models/page-data';
import { Warehouse } from './../../../models/warehouse';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { WarehouseService } from '../../../services/warehouse/warehouse.service';
import { CategoryService } from '../../../services/category/category.service';
import { InboundReceiptService } from '../../../services/inbound-receipt/inbound-receipt.service';
import { InboundReceipt } from '../../../models/inbound-receipt';
import { Inventory } from '../../../models/inventory';

@Component({
  selector: 'noc-inbound-receipt-table',
  templateUrl: './inbound-receipt-table.component.html',
  styleUrls: ['./inbound-receipt-table.component.css']
})
export class InboundReceiptTableComponent implements OnInit {
  public warehouses: Warehouse[];
  public selectedWarehouseId: number = -1;
  public startDate: Date;
  public endDate: Date;
  public items: InboundReceipt[];
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
  public readonly pageSizes: number[] = [5, 10, 20];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private warehouseService: WarehouseService,
    private categoryService: CategoryService,
    private inboundReceiptService: InboundReceiptService) {
    let date = new Date();
    let nowDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDate = new Date(nowDate.getTime() + 24 * 3600 * 1000);
    this.startDate = new Date(this.endDate.getTime() - 7 * 24 * 3600 * 1000);
  }

  ngOnInit() {
    this.page = 1;
    this.pageSize = 5;
    this.warehouses = []
    this.warehouseService.getSimples()
      .subscribe(data => {
        this.warehouses = data.isSuccess ? data.data as Warehouse[] : [];
      });
  }

  public newReceipt() {
    this.router.navigateByUrl('/workspace/inventory/new-inbound-receipt');
  }

  public receiptDetail(item: Inventory) {
    this.router.navigate(['../inbound-receipt-detail'], {
      relativeTo: this.activatedRoute,
      queryParams: {
        id: item.id
      }
    });
  }

  public search() {
    this.getItems();
  }

  public pageChange(page: number) {
    this.page = page;
    this.getItems();
  }

  private getItems() {
    this.inboundReceiptService.getInboundReceipts({
      warehouse: this.selectedWarehouseId,
      startDate: new Date(this.startDate.getTime() - 12 * 3600 * 1000),
      endDate: new Date(this.endDate.getTime() + 12 * 3600 * 1000),
      page: this.page,
      pageSize: this.pageSize
    }).subscribe(data => {
      if (data.isSuccess) {
        const pageData = data.data as PageData<InboundReceipt>;
        this.total = pageData.total;
        this.items = pageData.data
      }
    });
  }
}
