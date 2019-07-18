import { Router } from '@angular/router';
import { Injectable, ErrorHandler } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginModel } from '../../account/login/login.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject, Observable, Subscription } from 'rxjs';
import { catchError, retry, map } from 'rxjs/operators';
import { UserProfileModel } from 'app/account/user-profile/user-profile.model';
import { userInfo } from 'app/apps/chat/sidenavs/left/user/userInfo.model';

const loginUrl = environment.authLoginUrl;
const authGoogleUrl = environment.authGoogleUrl;
const authFBUrl = environment.authFacebookUrl;
const httpOptions = {
    headers: new HttpHeaders({
        'Accept': 'text/html, application/xhtml+xml, */*',
        'Content-Type': 'application/json'
    }),
    responseType: 'text' as 'json'
};
@Injectable()
export class UserProfileService {

    private userProfile: UserProfileModel;
    constructor(private router: Router,
                private authService: AuthService,
                private http: HttpClient) {
        this.userProfile = new UserProfileModel();
    }

    public displayNameSub$: Subject<string> = new Subject<string>();

    logIn(loginModel: LoginModel): Observable<void> {
        const userName = loginModel.userName;
        const password = loginModel.password;
        if (userName !== '' && password !== '') {
            const body = {
                Username: userName,
                Password: password
            };

            return this.http.post<any>(loginUrl, body, httpOptions)
                .pipe(
                    map((token: string) => {
                        this.parseJwtToken(token);
                        this.router.navigate(['chat']);
                    })
                );
        }
    }

    logInGoogle(googleAccessToken: string): Subscription {

        return this.http.post<string>(authGoogleUrl + `?accessToken=${googleAccessToken}`, null, httpOptions).pipe(
            map((token: string) => {
                this.parseJwtToken(token);
                this.router.navigate(['chat']);
            })
        ).subscribe();
    }

    logInFacebook(facebookAccessToken: string): Subscription {
        return this.http.post<string>(authFBUrl + `?token=${facebookAccessToken}`, null, httpOptions)
        .pipe(
            map((token: string) => {
                this.parseJwtToken(token);
                this.router.navigate(['chat']);
            })
        ).subscribe();
    }

    logOut(): Promise<boolean> {
        this.authService.clearToken();
        return this.router.navigate(['account', 'login']);
    }

    public parseJwtToken(token: string): void {
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
        
    }

    loadStoredUserProfile(): void {
        const token = this.authService.getToken();
        if (token.length > 0) {
            this.parseJwtToken(token);
        }
    }


}
