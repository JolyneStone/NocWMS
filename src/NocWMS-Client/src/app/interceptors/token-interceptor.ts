import { HttpHandler } from '@angular/common/http/src/backend';
import { HttpInterceptor } from "@angular/common/http/src/interceptor";
import { AuthService } from "../services/auth/auth.service";
import { Observable } from "rxjs";
import { HttpEvent } from "@angular/common/http/src/response";
import { HttpRequest } from "@angular/common/http/src/request";
import { Inject, Injectable } from '@angular/core';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(
        private authService: AuthService
    ) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.authService.token !== null) {
            const authHeader = this.authService.authorizationHeader;
            // interceptor不能修改request, 所以只能clone.
            const authReq = req.clone({
                headers: req.headers.append('Authorization', authHeader),
                reportProgress: req.reportProgress,
                responseType: req.responseType,
                withCredentials: req.withCredentials,
                body: req.body,
                method: req.method,
                params: req.params,
                url: req.url
            });
            //const authReq = req.clone({ headers: req.headers.set('Authorization', authHeader) });
            console.log(authReq);
            return next.handle(authReq);
        }
        else {
            next.handle(req);
        }
    }
}
