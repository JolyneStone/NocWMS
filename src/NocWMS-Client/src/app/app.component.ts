import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationStart, NavigationEnd, NavigationError, NavigationCancel } from '@angular/router';
import { EventBusService } from './services/event-bus/event-bus.service';
import { ANIMATION_TYPES } from 'ngx-loading';
import 'rxjs/Rx';

@Component({
  selector: 'noc-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  public loading = false;
  private globalClickCallbackFn: Function;
  //private loginSuccessCallbackFn: Function;

  constructor(
    public router: Router,
    public activatedRoute: ActivatedRoute,
    private eventBusService: EventBusService
  ) {
  }

  ngOnInit() {
    // setTimeout(() => {
    //   this.loading = false;
    // }, 0);
    this.eventBusService.showGlobalLoading.subscribe((value: boolean) => {
      this.loading = value;
    });

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.eventBusService.showGlobalLoading.next(true);
      }
      if (event instanceof NavigationEnd ||
        event instanceof NavigationError ||
        event instanceof NavigationCancel) {
        this.eventBusService.showGlobalLoading.next(false);
      }
    });
  }

  ngOnDestroy() {
    if (this.globalClickCallbackFn) {
      this.globalClickCallbackFn();
    }
  }
}
