import { UserProfileService } from './identity/userprofile.service';
import { Injectable } from '@angular/core';

import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Injectable()
export class AuthGuardService implements CanActivate, CanActivateChild {
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        var isAuthenticated = this.authService.isAuthenticated();
        if (isAuthenticated) {
            return true;
        }
        else {
            this.router.navigate(['/account/login']);
            return false;
        }
    }

    canActivateChild(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.canActivate(next, state);
    }

    constructor(
        private router: Router,
        private authService: AuthService) {

    }

}
