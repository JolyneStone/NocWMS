import { CategoryEditComponent } from './components/category-edit/category-edit.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { CustomerEditComponent } from './components/customer-edit/customer-edit.component';
import { StaffEditComponent } from './components/staff-edit/staff-edit.component';
import { VendorEditComponent } from './components/vendor-edit/vendor-edit.component';
import { WarehouseEditComponent } from './components/warehouse-edit/warehouse-edit.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { StaffFormComponent } from './components/staff-form/staff-form.component';
import { CustomerFormComponent } from './components/customer-form/customer-form.component';
import { VendorFormComponent } from './components/vendor-form/vendor-form.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { WarehouseFormComponent } from './components/warehouse-form/warehouse-form.component';
import { StaffTableComponent } from './components/staff-table/staff-table.component';
import { CustomerTableComponent } from './components/customer-table/customer-table.component';
import { VendorTableComponent } from './components/vendor-table/vendor-table.component';
import { CategoryTableComponent } from './components/category-table/category-table.component';
import { WarehouseTableComponent } from './components/warehouse-table/warehouse-table.component';
import { WarehouseMapComponent } from './components/warehouse-map/warehouse-map.component';
import { BasicDataComponent } from './basic-data.component';
import { WarehouseCellFormComponent } from './components/warehouse-cell-form/warehouse-cell-form.component';
import { WarehouseCellTableComponent } from './components/warehouse-cell-table/warehouse-cell-table.component';
import { WarehouseCellEditComponent } from './components/warehouse-cell-edit/warehouse-cell-edit.component';
import { ProductTableComponent } from './components/product-table/product-table.component';

export const basicDataRoutes: Routes = [{
  path: '',
  component: BasicDataComponent,
  children: [
    { path: '', redirectTo: 'warehouse-table', pathMatch: 'full' },
    { path: 'warehouse-map', component: WarehouseMapComponent },
    { path: 'warehouse-table', component: WarehouseTableComponent },
    { path: 'warehouse-cell-table', component: WarehouseCellTableComponent },
    { path: 'category-table', component: CategoryTableComponent },
    { path: 'vendor-table', component: VendorTableComponent },
    { path: 'customer-table', component: CustomerTableComponent },
    { path: 'staff-table', component: StaffTableComponent },
    { path: 'product-table', component: ProductTableComponent },
    { path: 'warehouse-form', component: WarehouseFormComponent },
    { path: 'warehouse-cell-table', component: WarehouseCellFormComponent },
    { path: 'vendor-form', component: VendorFormComponent },
    { path: 'customer-form', component: CustomerFormComponent },
    { path: 'staff-form', component: StaffFormComponent },
    { path: 'warehouse-cell-form', component: WarehouseCellFormComponent },
    { path: 'category-form', component: CategoryFormComponent },
    { path: 'product-form', component: ProductFormComponent },
    { path: 'staff-edit', component: StaffEditComponent },
    { path: 'customer-edit', component: CustomerEditComponent },
    { path: 'vendor-edit', component: VendorEditComponent },
    { path: 'warehouse-edit', component: WarehouseEditComponent },
    { path: 'warehouse-cell-edit', component: WarehouseCellEditComponent },
    { path: 'category-edit', component: CategoryEditComponent },
    { path: 'product-edit', component: ProductEditComponent }
  ]
}];


@NgModule({
  imports: [
    RouterModule.forChild(basicDataRoutes)
  ],
  exports: [RouterModule],
  declarations: []
})
export class BasicDataRoutingModule { }
