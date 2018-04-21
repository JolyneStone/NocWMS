import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkspaceComponent } from './workspace.component';

export const workspaceRoutes: Routes = [
  {
    path: '',
    component: WorkspaceComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', loadChildren: '../dashboard/dashboard.module#DashboardModule' },
      { path: 'inventory', loadChildren: '../inventory/inventory.module#InventoryModule' },
      { path: 'basic-data', loadChildren: '../basic-data/basic-data.module#BasicDataModule' },
      { path: 'sys', loadChildren: '../sys/sys.module#SysModule' },
      { path: 'profile', loadChildren: '../profile/profile.module#ProfileModule' }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(workspaceRoutes)
  ],
  exports: [RouterModule],
  declarations: []
})
export class WorkspaceRoutingModule { }
