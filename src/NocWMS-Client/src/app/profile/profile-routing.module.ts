import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ProfileComponent } from './profile.component';
import { ProfileInfoComponent } from './components/profile-info/profile-info.component';

export const profileRoutes: Routes = [
    {
        path: '',
        component: ProfileComponent,
        children: [
            { path: '', redirectTo: 'info', pathMatch: 'full' },
            { path: 'info', component: ProfileInfoComponent }
        ]
    }
]

@NgModule({
    imports: [
        RouterModule.forChild(profileRoutes)
    ],
    exports: [RouterModule],
    declarations: []
})
export class ProfileRoutingModule { }
