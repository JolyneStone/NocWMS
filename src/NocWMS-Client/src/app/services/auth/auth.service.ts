import { Observable, ReplaySubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { User, Log, UserManager } from 'oidc-client';
import { nocwmsSetting } from '../../nocwms-settings';

Log.logger = console;
Log.level = Log.DEBUG;

@Injectable()
export class AuthService {

  private _user: User;

  private readonly _authType = 'Bearer';
  // userKey字符串是oidc-client在localStorage默认存放用户信息的key, 这个可以通过oidc-client的配置来更改.
  // 可以在oidc-client/src/UserManager.js 的_userStoreKey中看到这个key
  private readonly _userKey = `noc-user:${nocwmsSetting.authConfig.authority}:${nocwmsSetting.authConfig.client_id}`;
  private readonly manager: UserManager = new UserManager(nocwmsSetting.authConfig);

  public readonly authenticationChallenge = new ReplaySubject<boolean>(1);

  constructor(
  ) {
    this.manager.clearStaleState();
    this.manager.events.addUserLoaded((user) => {
      console.log("addUserLoaded");
      this.updateUser(user);
    });

    this.manager.events.addUserUnloaded(() => {
      console.log("addUserUnloaded");
      this.updateUser();
    });

    // this.manager.events.addUserSignedOut(() => {
    //   console.log("addUserSignedOut");
    //   this.updateUser();
    // });

    this.manager.getUser().then(user => {
      console.log("auth manager");
      if (user) {
        if (user.expired) {
          this.renewToken();
        } else {
          this.updateUser(user);
        }
      } else {
        user = this.user;
        if (user) {
          if (user.expired) {
            this.renewToken();
          }
        }

        this.updateUser(user);
      }
    }).catch((err) => {
      console.log(err);
    });
  }

  public get authType(): string {
    return this._authType;
  }

  public get token(): string | null {
    const user = this.user;
    if (user) {
      return user.access_token;
    }

    return null;
  }

  public get authorizationHeader(): string | null {
    const token = this.token;
    if (token) {
      return `Bearer ${token}`;
    }

    return null;
  }

  get user(): User {
    if (!this._user) {
      this._user = JSON.parse(localStorage.getItem(this._userKey)) as User;
    }

    return this._user;
  }

  private renewToken() {
    this.manager.signinSilent()
      .then((user) => {
        console.log("renewToken");
        this.updateUser(user);
      })
      .catch(error => {
        console.log(error);
      });
  }

  private updateUser(user: User = null) {
    this._user = user;
    console.log(user);
    if (user) {
      localStorage.setItem(this._userKey, JSON.stringify(user));
      this.authenticationChallenge.next(true);
    } else {
      localStorage.removeItem(this._userKey);
      this.authenticationChallenge.next(false);
    }
  }


  public signin() {
    // 跳转到Authorization Server的登录页面
    this.manager.signinRedirect();
  }

  // public loginCallback(): Promise<User> {
  //   return this.manager.signinRedirectCallback()
  //     .then((user: User) => {
  //       this.authenticationChallenge.next(user !== null);
  //       localStorage.setItem(this._userKey, JSON.stringify(user));
  //       return user;
  //     });
  // }

  public signinCallback(): Observable<User> {
    return Observable.create(observer => {
      Observable.fromPromise(this.manager.signinRedirectCallback())
        .subscribe((user: User) => {
          console.log("signinCallback");
          this.updateUser(user);
          observer.next(user);
          observer.complete();
        });
    });
  }

  public signout() {
    // 跳转到Authorization Server的退出页面
    this.manager.signoutRedirect()
  }

  public signoutCallback(): Observable<void> {
    return Observable.create(observer => {
      Observable.fromPromise(this.manager.signoutRedirectCallback())
        .subscribe(() => {
          console.log("signoutCallback");
          this.updateUser(null);
          this.manager.events.removeAccessTokenExpired(() => { });
          observer.next(null);
          observer.complete();
        })
    })
  }

  public checkUser() {
    this.tryGetUser().subscribe((user: User) => {
      this._user = user;
      this.authenticationChallenge.next(user !== null);
    }, e => {
      this.authenticationChallenge.next(false);
    });
  }


  public tryGetUser() {
    return Observable.fromPromise(this.manager.getUser()
      .then((user: User) => {
        return user;
      }));
  }
}