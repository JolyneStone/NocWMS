import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { SysRoutingModule } from './sys-routing.module';
import { SysMonitorComponent } from './sys-monitor/sys-monitor.component';
import { SysComponent } from './sys.component';
import { NgxEchartsModule } from 'ngx-echarts';

@NgModule({
  imports: [
    SharedModule,
    SysRoutingModule,
    NgxEchartsModule
  ],
  declarations: [
    SysComponent, SysMonitorComponent
  ]
})
export class SysModule { }
