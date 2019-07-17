import * as tslib_1 from "tslib";
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
const authUrl = environment.authUrl;
let TokenHttpInterceptor = class TokenHttpInterceptor {
    constructor(authService) {
        this.authService = authService;
    }
    intercept(request, next) {
        const securityToken = this.authService.getToken();
        if (request.url === authUrl) {
            request.headers.set('Content-Type', 'application/json; charset=UTF-8');
            return next.handle(request);
        }
        else if (securityToken.length > 0) {
            request = request.clone({
                setHeaders: {
                    'Content-Type': 'application/json; charset=UTF-8',
                    'Authorization': `Bearer ${securityToken}`
                }
            });
            return next.handle(request);
        }
    }
};
TokenHttpInterceptor = tslib_1.__decorate([
    Injectable(),
    tslib_1.__metadata("design:paramtypes", [AuthService])
], TokenHttpInterceptor);
export { TokenHttpInterceptor };
//# sourceMappingURL=token.httpinterceptor.js.map