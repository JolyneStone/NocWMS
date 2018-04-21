import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkspaceRoutingModule } from './workspace-routing.module';
import { WorkspaceComponent } from './workspace.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';

@NgModule({
  imports: [
    SharedModule,
    WorkspaceRoutingModule
  ],
  declarations: [
    WorkspaceComponent,
    NavbarComponent,
    FooterComponent,
  ]
})
export class WorkspaceModule { }
