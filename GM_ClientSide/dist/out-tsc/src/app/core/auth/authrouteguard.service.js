import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
let AuthRouteGuardService = class AuthRouteGuardService {
    constructor(router, authService) {
        this.router = router;
        this.authService = authService;
    }
    canActivate(route, state) {
        const isAuthenticated = this.authService.isAuthenticated();
        if (isAuthenticated) {
            return true;
        }
        else {
            this.router.navigate(['/account/login']);
            return false;
        }
    }
    canActivateChild(next, state) {
        return this.canActivate(next, state);
    }
};
AuthRouteGuardService = tslib_1.__decorate([
    Injectable(),
    tslib_1.__metadata("design:paramtypes", [Router,
        AuthService])
], AuthRouteGuardService);
export { AuthRouteGuardService };
//# sourceMappingURL=authrouteguard.service.js.map