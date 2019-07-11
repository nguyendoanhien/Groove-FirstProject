import { Injectable } from '@angular/core';

import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthRouteGuardService implements CanActivate, CanActivateChild {
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const isAuthenticated = this.authService.isAuthenticated();
        if (isAuthenticated) {
            return true;
        }
        else {
            this.router.navigate(['/account/login']);
            return false;
        }
    }

    canActivateChild(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(next, state);
    }

    constructor(
        private router: Router,
        private authService: AuthService) {

    }

}
