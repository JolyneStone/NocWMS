import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { OutboundReceiptService } from '../../../services/outbound-receipt/outbound-receipt.service';
import { OutboundReceipt } from '../../../models/outbound-receipt';
import { OutboundReceiptItem } from '../../../models/outbound-receipt-item';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Warehouse } from '../../../models/warehouse';
import { Category } from '../../../models/category';
import { WarehouseService } from '../../../services/warehouse/warehouse.service';
import { WarehouseCellService } from '../../../services/warehouse-cell/warehouse-cell.service';
import { CategoryService } from '../../../services/category/category.service';
import { ProductService } from '../../../services/product/product.service';
import { ToastrService } from 'ngx-toastr';
import { WarehouseCell } from '../../../models/warehouse-cell';
import { Product } from '../../../models/product';

@Component({
  selector: 'noc-new-outbound-receipt',
  templateUrl: './new-outbound-receipt.component.html',
  styleUrls: ['./new-outbound-receipt.component.css']
})
export class NewOutboundReceiptComponent implements OnInit {

  public outboundReceipt: OutboundReceipt = new OutboundReceipt({ warehouseId: -1, outboundReceiptItems: [] });
  public outboundReceiptItem: OutboundReceiptItem;
  private modalRef: NgbModalRef;
  @ViewChild('content')
  private content: ElementRef;
  public warehouses: Warehouse[];
  public categories: Category[];
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private outboundReceiptService: OutboundReceiptService,
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
    this.router.navigateByUrl('/workspace/inventory/outbound-receipt-table');
  }

  public onSubmit() {
    this.outboundReceiptService.addOutboundReceipt(this.outboundReceipt)
      .subscribe(data => {
        if (data.isSuccess) {
          this.toastrService.success("添加入库单成功", "成功");
          this.outboundReceipt = new OutboundReceipt({ createTime: new Date().toLocaleString(), warehouseId: -1 });
        } else {
          this.toastrService.error(data.message, "失败");
        }
      });
  }

  public open() {
    if (this.outboundReceipt.warehouseId === -1) {
      this.toastrService.info("请先选择仓库", "提示");
    } else {
      this.outboundReceiptItem = new OutboundReceiptItem();
      this.modalRef = this.modalService.open(this.content, {
        size: 'lg'
      });
      this.modalRef.result.then((result) => {
        if (result === 'submit') {
          this.warehouseCellService.getWarehouseCellByName(this.outboundReceipt.warehouseId, this.outboundReceiptItem.storeCell).withLatestFrom(this.productService.getProductByName(this.outboundReceiptItem.productName), (cellData, productData) => {
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
              this.outboundReceiptItem.storeCell = warehouseCell.cellName;
              this.outboundReceiptItem.warehouseCellId = warehouseCell.id;

              let product = data.productDate.data as Product;
              this.outboundReceiptItem.categoryName = this.categories.find(x =>
                x.id == this.outboundReceiptItem.categoryId)
                .categoryName;
              this.outboundReceiptItem.productId = product.id;
              this.outboundReceiptItem.productUnit = product.unit;
              this.outboundReceiptItem.productModel = product.model;
              this.outboundReceiptItem.productPrice = product.sellPrice;
              this.outboundReceiptItem.productSpec = product.spec;
              this.outboundReceipt.outboundReceiptItems.push(this.outboundReceiptItem);
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
