import { environment } from "../../../environments/environment";
import { Injectable } from "@angular/core";
import { tap } from "rxjs/operators";
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from "@angular/common/http";
import { AuthService } from "./auth.service";
import { Observable } from "rxjs";
import { Router } from "@angular/router";

const loginUrl = environment.authLoginUrl;

@Injectable()
export class TokenHttpInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService, private _router: Router) {

    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const securityToken = this.authService.getToken();
        if (request.url === loginUrl) {
            request.headers.set("Content-Type", "application/json; charset=UTF-8");
            return next.handle(request);
        } else if (request.url === "https://api.cloudinary.com/v1_1/groovemessenger/upload") {
            return next.handle(request);
        } else if (securityToken.length > 0) {
            request = request.clone({
                setHeaders: {
                    'Content-Type': "application/json; charset=UTF-8",
                    'Authorization': `Bearer ${securityToken}`
                }
            });
            return next.handle(request).pipe(
                tap(
                    succ => {},
                    err => {
                        if (err.status == 401) {
                            localStorage.removeItem("token");
                            this._router.navigate(["account", "login"]);
                        }
                    }));
        } else {
            return next.handle(request);
        }
    }
}