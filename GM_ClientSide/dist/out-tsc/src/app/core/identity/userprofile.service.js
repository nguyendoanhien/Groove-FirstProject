import * as tslib_1 from "tslib";
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
// <<<<<<< HEAD
// import { Subject, Observable, of } from 'rxjs';
// =======
import { Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserProfileModel } from 'app/account/user-profile/user-profile.model';
const authUrl = environment.authUrl;
const authGoogleUrl = environment.authGoogleUrl;
const authFBUrl = environment.authFacebookUrl;
const httpOptions = {
    headers: new HttpHeaders({
        'Accept': 'text/html, application/xhtml+xml, */*',
        'Content-Type': 'application/json'
    }),
    responseType: 'text'
};
let UserProfileService = class UserProfileService {
    constructor(router, authService, http) {
        this.router = router;
        this.authService = authService;
        this.http = http;
        this.displayNameSub$ = new Subject();
        this.userProfile = new UserProfileModel();
    }
    logIn(loginModel) {
        const userName = loginModel.userName;
        const password = loginModel.password;
        if (userName !== '' && password !== '') {
            const body = {
                Username: userName,
                Password: password
            };
            const httpOptions = {
                headers: new HttpHeaders({
                    'Accept': 'text/html, application/xhtml+xml, */*',
                    'Content-Type': 'application/json'
                }),
                responseType: "text"
            };
            return this.http.post(authUrl, body, httpOptions)
                .pipe(map((token) => {
                this.parseJwtToken(token);
                this.router.navigate(["apps", "chat"]);
            }));
        }
    }
    logInGoogle(googleAccessToken) {
        return this.http.post(authGoogleUrl + `?accessToken=${googleAccessToken}`, null, httpOptions).pipe(map((token) => {
            this.parseJwtToken(token);
            this.router.navigate(["apps", "chat"]);
        })).subscribe();
    }
    logInFacebook(facebookAccessToken) {
        return this.http.post(authFBUrl + `?token=${facebookAccessToken}`, null, httpOptions)
            .pipe(map((token) => {
            this.parseJwtToken(token);
            this.router.navigate(['apps', 'chat']);
        })).subscribe();
    }
    logOut() {
        this.authService.clearToken();
        return this.router.navigate(['account', 'login']);
    }
    parseJwtToken(token) {
        const jwt = token;
        const jwtHelper = new JwtHelperService();
        const decodedJwt = jwtHelper.decodeToken(jwt);
        this.authService.setToken(jwt);
        const userProfileModel = new UserProfileModel();
        userProfileModel.UserName = decodedJwt.UserName;
        userProfileModel.DisplayName = decodedJwt.DisplayName;
        userProfileModel.SecurityAccessToken = jwt;
        this.userProfile = userProfileModel;
        this.displayNameSub$.next(this.userProfile.DisplayName);
        console.log(this.userProfile.DisplayName);
    }
    loadStoredUserProfile() {
        let token = this.authService.getToken();
        if (token.length > 0) {
            this.parseJwtToken(token);
        }
    }
};
UserProfileService = tslib_1.__decorate([
    Injectable(),
    tslib_1.__metadata("design:paramtypes", [Router,
        AuthService,
        HttpClient])
], UserProfileService);
export { UserProfileService };
//# sourceMappingURL=userprofile.service.js.map