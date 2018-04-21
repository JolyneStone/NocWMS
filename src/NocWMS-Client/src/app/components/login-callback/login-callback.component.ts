import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'Noc-login-callback',
  template: '',
  styles: []
})
export class LoginCallbackComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.authService.signinCallback().subscribe(user => {
      if (!user) {
        console.log("can't found user");
      }

      this.router.navigate(['/'], { replaceUrl: true });
    }, err => {
      console.log(err);
    });
  }
}
