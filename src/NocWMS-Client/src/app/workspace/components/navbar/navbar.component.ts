import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../../../models/userinfo';
import { AuthService } from '../../../services/auth/auth.service';
import { UserService } from '../../../services/user/user.service';
import { EventBusService } from '../../../services/event-bus/event-bus.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'noc-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public isCollapsed = false;
  public currentUser: UserInfo;

  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
    private evetnBusService: EventBusService,
    private toastrService: ToastrService
  ) { }

  ngOnInit() {
    this.evetnBusService.topToggleBtn.subscribe(value => this.isCollapsed = value);
    this.authService.authenticationChallenge.subscribe((isAuthentication) => {
      if (isAuthentication) {
        this.userService.attach(this.authService.user)
          .subscribe(userInfo => {
            this.currentUser = userInfo;
            if (!this.currentUser.isCompleted) {
              this.toastrService.warning("请完善您的职工信息", "提示");
            }
          });
      } else {
        console.log("not user");
      }
    });
  }

  public onSignin() {
    this.authService.signin();
  }

  public onSignout() {
    this.authService.signout();
  }
}
