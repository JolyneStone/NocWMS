import { AuthGuard } from './guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoutCallbackComponent } from './components/logout-callback/logout-callback.component';
import { LoginCallbackComponent } from './components/login-callback/login-callback.component';

export const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'workspace',
    pathMatch: 'full'
  },
  {
    path: 'workspace',
    loadChildren: './workspace/workspace.module#WorkspaceModule',
    canActivate: [AuthGuard]
  },
  {
    path: 'login-callback',
    component: LoginCallbackComponent
  },
  {
    path: 'logout-callback',
    component: LogoutCallbackComponent
  },
  {
    path: '**',
    redirectTo: 'workspace',
    pathMatch: 'full'
  },
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule { }
