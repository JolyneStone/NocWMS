import { WarehouseCell } from './../../../models/warehouse-cell';
import { Product } from './../../../models/product';
import { WarehouseCellService } from './../../../services/warehouse-cell/warehouse-cell.service';
import { ProductService } from './../../../services/product/product.service';
import { CategoryService } from './../../../services/category/category.service';
import { Category } from './../../../models/category';
import { InboundReceiptItem } from './../../../models/inbound-receipt-item';
import { ToastrService } from 'ngx-toastr';
import { dashboardRoutes } from './../../../dashboard/dashboard-routing.module';
import { Vendor } from './../../../models/vendor';
import { Warehouse } from './../../../models/warehouse';
import { VendorService } from './../../../services/vendor/vendor.service';
import { WarehouseService } from './../../../services/warehouse/warehouse.service';
import { InboundReceipt } from './../../../models/inbound-receipt';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { InboundReceiptService } from '../../../services/inbound-receipt/inbound-receipt.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'noc-new-inbound-receipt',
  templateUrl: './new-inbound-receipt.component.html',
  styleUrls: ['./new-inbound-receipt.component.css']
})
export class NewInboundReceiptComponent implements OnInit {
  public inboundReceipt: InboundReceipt = new InboundReceipt({ warehouseId: -1, inboundReceiptItems: [] });
  public inboundReceiptItem: InboundReceiptItem;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public warehouses: Warehouse[];
  public categories: Category[];
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private inboundReceiptService: InboundReceiptService,
    private warehouseService: WarehouseService,
    private warehouseCellService: WarehouseCellService,
    private categoryService: CategoryService,
    private productService: ProductService,
    private toastrService: ToastrService,
    private modalService: NgbModal
  ) {
  }

  ngOnInit() {
    this.warehouseService.getSimples()
      .subscribe(data => {
        if (data.isSuccess) {
          this.warehouses = data.data as Warehouse[];
        }
      });

    this.categoryService.getSimples()
      .subscribe(data => {
        if (data.isSuccess) {
          this.categories = data.data as Category[];
        }
      });
  }

  public returnToTable() {
    this.router.navigateByUrl('/workspace/inventory/inbound-receipt-table');
  }

  public onSubmit() {
    this.inboundReceiptService.addInboundReceipt(this.inboundReceipt)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加入库单成功", "成功");
          this.inboundReceipt = new InboundReceipt({ createTime: new Date().toLocaleString(), warehouseId: -1 });
        }else {
          this.toastrService.error(data.message, "失败");
        }
      });
  }

  public open() {
    if (this.inboundReceipt.warehouseId === -1) {
      this.toastrService.info("请先选择仓库", "提示");
    } else {
      this.inboundReceiptItem = new InboundReceiptItem();
      this.modalRef = this.modalService.open(this.content, {
        size: 'lg'
      });
      this.modalRef.result.then((result) => {
        if (result === 'submit') {
          this.warehouseCellService.getWarehouseCellByName(this.inboundReceipt.warehouseId, this.inboundReceiptItem.storeCell).withLatestFrom(this.productService.getProductByName(this.inboundReceiptItem.productName), (cellData, productData) => {
            return {
              'cellData': cellData,
              'productDate': productData
            }
          })
            .subscribe(data => {
              if (!data.cellData.isSuccess) {
                this.toastrService.error(data.cellData.message);
                return;
              }
              if (!data.productDate.isSuccess) {
                this.toastrService.error(data.productDate.message);
                return;
              }

              let warehouseCell = data.cellData.data as WarehouseCell;
              this.inboundReceiptItem.storeCell = warehouseCell.cellName;
              this.inboundReceiptItem.warehouseCellId = warehouseCell.id;

              let product = data.productDate.data as Product;
              this.inboundReceiptItem.categoryName = this.categories.find(x =>
                x.id == this.inboundReceiptItem.categoryId)
                .categoryName;
              this.inboundReceiptItem.productId = product.id;
              this.inboundReceiptItem.productUnit = product.unit;
              this.inboundReceiptItem.productModel = product.model;
              this.inboundReceiptItem.productPrice = product.sellPrice;
              this.inboundReceiptItem.productSpec = product.spec;
              this.inboundReceipt.inboundReceiptItems.push(this.inboundReceiptItem);
            });
        }
      });
    }
  }

  public close(info: string | null) {
    if (info) {
      this.modalRef.close(info);
    } else {
      this.modalRef.dismiss();
    }
  }
}