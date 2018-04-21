import { InventoryEditComponent } from './components/inventory-edit/inventory-edit.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { InventoryComponent } from './inventory.component';
import { InventoryTableComponent } from './components/inventory-table/inventory-table.component';
import { InventoryItemDetailComponent } from './components/inventory-item-detail/inventory-item-detail.component';
import { InboundReceiptTableComponent } from './components/inbound-receipt-table/inbound-receipt-table.component';
import { InboundReceiptDetailComponent } from './components/inbound-receipt-detail/inbound-receipt-detail.component';
import { NewInboundReceiptComponent } from './components/new-inbound-receipt/new-inbound-receipt.component';
import { OutboundReceiptTableComponent } from './components/outbound-receipt-table/outbound-receipt-table.component';
import { OutboundReceiptDetailComponent } from './components/outbound-receipt-detail/outbound-receipt-detail.component';
import { NewOutboundReceiptComponent } from './components/new-outbound-receipt/new-outbound-receipt.component';

export const inventoryRoutes: Routes = [{
  path: '',
  component: InventoryComponent,
  children: [
    { path: '', redirectTo: 'inventorytable', pathMatch: 'full' },
    { path: 'inventory-table', component: InventoryTableComponent },
    { path: 'inventory-item-detail', component: InventoryItemDetailComponent },
    { path: 'inventory-edit', component: InventoryEditComponent },
    { path: 'inbound-receipt-table', component: InboundReceiptTableComponent },
    { path: 'inbound-receipt-detail', component: InboundReceiptDetailComponent },
    { path: 'new-inbound-receipt', component: NewInboundReceiptComponent },
    { path: 'outbound-receipt-table', component: OutboundReceiptTableComponent },
    { path: 'outbound-receipt-detail', component: OutboundReceiptDetailComponent },
    { path: 'new-outbound-receipt', component: NewOutboundReceiptComponent }
  ]
}];

@NgModule({
  imports: [
    RouterModule.forChild(inventoryRoutes)
  ],
  exports: [RouterModule],
  declarations: []
})
export class InventoryRoutingModule { }
