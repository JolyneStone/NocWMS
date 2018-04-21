import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { SysMonitorComponent } from './sys-monitor/sys-monitor.component';
import { SysComponent } from './sys.component';

export const sysRoutes: Routes = [{
    path: '',
    component: SysComponent,
    children: [
        { path: '', redirectTo: 'sysmonitor', pathMatch: 'full' },
        { path: 'sysmonitor', component: SysMonitorComponent },
    ]
}];

@NgModule({
    imports: [
        RouterModule.forChild(sysRoutes),
    ],
    exports: [RouterModule],
    declarations: []
})
export class SysRoutingModule { }
