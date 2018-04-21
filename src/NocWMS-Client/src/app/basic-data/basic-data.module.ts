import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { BasicDataRoutingModule } from './basic-data-routing.module';
import { CategoryTableComponent } from './components/category-table/category-table.component';
import { VendorTableComponent } from './components/vendor-table/vendor-table.component';
import { CustomerTableComponent } from './components/customer-table/customer-table.component';
import { StaffTableComponent } from './components/staff-table/staff-table.component';
import { WarehouseFormComponent } from './components/warehouse-form/warehouse-form.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { VendorFormComponent } from './components/vendor-form/vendor-form.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { StaffFormComponent } from './components/staff-form/staff-form.component';
import { WarehouseMapComponent } from './components/warehouse-map/warehouse-map.component';
import { WarehouseTableComponent } from './components/warehouse-table/warehouse-table.component';
import { BasicDataComponent } from './basic-data.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxEchartsModule } from 'ngx-echarts';
import { StaffEditComponent } from './components/staff-edit/staff-edit.component';
import { CustomerEditComponent } from './components/customer-edit/customer-edit.component';
import { VendorEditComponent } from './components/vendor-edit/vendor-edit.component';
import { WarehouseCellMapComponent } from './components/warehouse-cell-map/warehouse-cell-map.component';
import { WarehouseCellTableComponent } from './components/warehouse-cell-table/warehouse-cell-table.component';
import { WarehouseCellEditComponent } from './components/warehouse-cell-edit/warehouse-cell-edit.component';
import { WarehouseCellFormComponent } from './components/warehouse-cell-form/warehouse-cell-form.component';
import { WarehouseNavComponent } from './components/warehouse-nav/warehouse-nav.component';
import { CategoryEditComponent } from './components/category-edit/category-edit.component';
import { ProductTableComponent } from './components/product-table/product-table.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';

@NgModule({
  imports: [
    SharedModule,
    BasicDataRoutingModule,
    ReactiveFormsModule,
    NgxEchartsModule
  ],
  declarations: [
    WarehouseMapComponent,
    WarehouseTableComponent,
    BasicDataComponent,
    CategoryTableComponent,
    VendorTableComponent,
    CustomerTableComponent,
    StaffTableComponent,
    WarehouseFormComponent,
    CategoryFormComponent,
    VendorFormComponent,
    CustomerFormComponent,
    StaffFormComponent,
    StaffEditComponent,
    CustomerEditComponent,
    VendorEditComponent,
    WarehouseCellMapComponent,
    WarehouseCellTableComponent,
    WarehouseCellEditComponent,
    WarehouseCellFormComponent,
    WarehouseNavComponent,
    CategoryEditComponent,
    ProductTableComponent,
    ProductFormComponent,
    ProductEditComponent
  ]
})
export class BasicDataModule { }
