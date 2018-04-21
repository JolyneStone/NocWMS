import { AuthService } from './../services/auth/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    this.authService.checkUser();
    return this.authService.authenticationChallenge.map((isAuthentication) => {
      console.log("guard", isAuthentication);
      if (isAuthentication) {
        return true;
      } else {
        this.authService.signin();
        return true
      }
    });
  }
}
