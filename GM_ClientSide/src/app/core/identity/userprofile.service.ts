import { Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from "../auth/auth.service";
import { environment } from "../../../environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { LoginModel } from "../../account/login/login.model";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Subject, Observable, Subscription } from "rxjs";
import { map } from "rxjs/operators";
import { UserProfileModel } from "app/account/user-profile/user-profile.model";
import { FacebookService } from 'ngx-facebook';
import { AuthService as AuthSocialService } from "angularx-social-login";
const loginUrl = environment.authLoginUrl;
const authGoogleUrl = environment.authGoogleUrl;
const authFBUrl = environment.authFacebookUrl;
const httpOptions = {
    headers: new HttpHeaders({
        'Accept': "text/html, application/xhtml+xml, */*",
        'Content-Type': "application/json"
    }),
    responseType: "text" as "json"
};

@Injectable()
export class UserProfileService {

    userProfile: UserProfileModel;

    constructor(private router: Router,
        private authService: AuthService,
        private http: HttpClient,
        private _fb: FacebookService,
        private _authService: AuthSocialService
    ) {
        this.userProfile = new UserProfileModel();
    }

    displayNameSub$ = new Subject<string>();

    logIn(loginModel: LoginModel): Observable<void> {
        const userName = loginModel.userName;
        const password = loginModel.password;
        if (userName !== "" && password !== "") {
            const body = {
                Username: userName,
                Password: password
            };

            return this.http.post<any>(loginUrl, body, httpOptions)
                .pipe(
                    map((token: string) => {
                        this.parseJwtToken(token);
                        this.router.navigate(["chat"]);
                    })
                );
        }
    }

    logInGoogle(googleAccessToken: string): Subscription {

        return this.http.post<string>(authGoogleUrl + `?accessToken=${googleAccessToken}`, null, httpOptions).pipe(
            map((token: string) => {
                this.parseJwtToken(token);
                this.router.navigate(["chat"]);
            })
        ).subscribe(
            (success) => console.log(`success la${success}`),
            (error) => console.log(`error la${error}`)
        );
    }

    logInFacebook(facebookAccessToken: string): Subscription {
        return this.http.post<string>(authFBUrl + `?token=${facebookAccessToken}`, null, httpOptions)
            .pipe(
                map((token: string) => {
                    this.parseJwtToken(token);
                    this.router.navigate(["chat"]);
                })
            ).subscribe();
    }

    logOut(): Promise<boolean> {


        // debugger;
        // this._authService.authState.subscribe((user) => {
        //     debugger;
        //     this.authService.clearToken();
        //     if (user != null)
        //         this._authService.signOut().then(() => console.log('signout'));

        // }, err => console.log('error'), () => console.log('finished'));
        this.authService.clearToken();
        this._fb.getLoginStatus().then((response) => {
            debugger;
            if (response.status === 'connected') {
                console.log('FB Connected')
                this._fb.logout().then((response) => {
                    console.log('Log out')
                });
            } else if (response.status === 'not_authorized') {
                console.log('FB Not Authorized')
            } else {
                console.log('FB Unconnected')
            }
        });

        // return Promise.resolve(null);
        return this.router.navigate(["account", "login"]);
    }

    parseJwtToken(token: string): void {
        const jwt = token;
        const jwtHelper = new JwtHelperService();
        const decodedJwt = jwtHelper.decodeToken(jwt);
        this.authService.setToken(jwt);

        const userProfileModel = new UserProfileModel();
        userProfileModel.UserName = decodedJwt.UserName;
        userProfileModel.Email = decodedJwt.UserName;
        userProfileModel.DisplayName = decodedJwt.DisplayName;
        userProfileModel.UserId = decodedJwt.UserId;
        userProfileModel.UserInfoId = decodedJwt.UserInfoId;
        userProfileModel.SecurityAccessToken = jwt;

        this.userProfile = userProfileModel;
        this.displayNameSub$.next(this.userProfile.DisplayName);

    }

    loadStoredUserProfile(): void {
        const token = this.authService.getToken();
        if (token.length > 0) {
            this.parseJwtToken(token);
        }
    }


}