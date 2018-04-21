import { ProfileRoutingModule } from './profile-routing.module';
import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { ProfileComponent } from './profile.component';
import { ProfileInfoComponent } from './components/profile-info/profile-info.component';
import { ProfileAvatarComponent } from './components/profile-avatar/profile-avatar.component';

@NgModule({
  imports: [
    SharedModule,
    ProfileRoutingModule
  ],
  declarations: [ProfileComponent, ProfileInfoComponent, ProfileAvatarComponent]
})
export class ProfileModule { }
