import { UserService } from './services/user/user.service';
import { AuthService } from './services/auth/auth.service';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { LoginCallbackComponent } from './components/login-callback/login-callback.component';
import { LogoutCallbackComponent } from './components/logout-callback/logout-callback.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthGuard } from './guards/auth.guard';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from './shared/shared.module';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { DropdownDirective } from './directives/dropdown.directive';
import { DropdownTriggerDirective } from './directives/dropdown-trigger.directive';

@NgModule({
  declarations: [
    AppComponent,
    LoginCallbackComponent,
    LogoutCallbackComponent,
    DropdownDirective,
    DropdownTriggerDirective
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    SharedModule,
    ToastrModule.forRoot(),
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.rectangleBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#ffffff',
      secondaryColour: '#ffffff',
      tertiaryColour: '#ffffff'
    }),
  ],
  providers: [
    AuthGuard,
    AuthService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
