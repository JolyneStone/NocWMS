import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'Noc-logout-callback',
  template: '',
  styles: []
})
export class LogoutCallbackComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    this.authService.signoutCallback();
    this.authService.authenticationChallenge.subscribe((isAuthenticated) => {
      if (!isAuthenticated) {
        this.router.navigate([''], { replaceUrl: true });
      }
    },
      (err) => {
        console.log(err);
      });
  }
}
