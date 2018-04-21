import { FormsModule } from '@angular/forms';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxEchartsModule } from 'ngx-echarts';
import { InventoryRoutingModule } from './inventory-routing.module';
import { InventoryComponent } from './inventory.component';
import { InventoryTableComponent } from './components/inventory-table/inventory-table.component';
import { InventoryItemDetailComponent } from './components/inventory-item-detail/inventory-item-detail.component';
import { InboundReceiptTableComponent } from './components/inbound-receipt-table/inbound-receipt-table.component';
import { InboundReceiptDetailComponent } from './components/inbound-receipt-detail/inbound-receipt-detail.component';
import { NewInboundReceiptComponent } from './components/new-inbound-receipt/new-inbound-receipt.component';
import { NewOutboundReceiptComponent } from './components/new-outbound-receipt/new-outbound-receipt.component';
import { OutboundReceiptDetailComponent } from './components/outbound-receipt-detail/outbound-receipt-detail.component';
import { OutboundReceiptTableComponent } from './components/outbound-receipt-table/outbound-receipt-table.component';
import { InventoryEditComponent } from '../inventory/components/inventory-edit/inventory-edit.component';

@NgModule({
  imports: [
    SharedModule,
    InventoryRoutingModule,
    NgxEchartsModule
  ],
  declarations: [
    InventoryComponent,
    InventoryTableComponent,
    InventoryItemDetailComponent,
    InboundReceiptTableComponent,
    InboundReceiptDetailComponent,
    NewInboundReceiptComponent,
    NewOutboundReceiptComponent,
    OutboundReceiptDetailComponent,
    OutboundReceiptTableComponent,
    InventoryEditComponent
  ]
})
export class InventoryModule { }
