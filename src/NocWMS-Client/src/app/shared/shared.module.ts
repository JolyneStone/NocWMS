import { UserService } from './../services/user/user.service';
import { AuthService } from './../services/auth/auth.service';
import { FormsModule } from '@angular/forms';
import { HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventBusService } from '../services/event-bus/event-bus.service';
import { CategoryService } from '../services/category/category.service';
import { WarehouseService } from '../services/warehouse/warehouse.service';
import { InventoryService } from '../services/inventory/inventory.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { InboundReceiptService } from '../services/inbound-receipt/inbound-receipt.service';
import { OutboundReceiptService } from '../services/outbound-receipt/outbound-receipt.service';
import { VendorService } from '../services/vendor/vendor.service';
import { CustomerService } from '../services/customer/customer.service';
import { GenderPipe } from '../pipes/gender.pipe';
import { StaffService } from '../services/staff/staff.service';
import { TokenInterceptor } from '../interceptors/token-interceptor';
import { CellStatusPipe } from '../pipes/cellStatus.pipe';
import { WarehouseEditComponent } from '../basic-data/components/warehouse-edit/warehouse-edit.component';
import { WarehouseCellService } from '../services/warehouse-cell/warehouse-cell.service';
import { ProductService } from '../services/product/product.service';
import { ReceiptTypePipe } from '../pipes/receipt-type.pipe';
import { NgbDateNativeAdapter } from '../services/NgbDateNativeAdapter/ngb-datenative-adapter';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    NgbModule.forRoot(),
    FormsModule
  ],
  exports: [
    CommonModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    GenderPipe,
    CellStatusPipe,
    ReceiptTypePipe
  ],
  declarations: [GenderPipe, CellStatusPipe, WarehouseEditComponent, ReceiptTypePipe],
  providers: [
    //AuthService,
    //UserService,
    EventBusService,
    CategoryService,
    WarehouseService,
    InventoryService,
    InboundReceiptService,
    OutboundReceiptService,
    VendorService,
    CustomerService,
    StaffService,
    WarehouseCellService,
    ProductService,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ]
})
export class SharedModule { }
