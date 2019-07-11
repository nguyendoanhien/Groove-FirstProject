import { Router } from '@angular/router';
import { Injectable, ErrorHandler } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginModel } from '../../account/login/login.model';
import { JwtHelperService } from '@auth0/angular-jwt';
// <<<<<<< HEAD
// import { Subject, Observable, of } from 'rxjs';
// =======
import { Subject, Observable, Subscription } from 'rxjs';
import { catchError, retry, map } from 'rxjs/operators';
import { UserProfileModel } from 'app/account/user-profile/user-profile.model';

const authUrl = environment.authUrl;
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

    logIn(loginModel: LoginModel) {
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
                responseType: "text" as 'json'
            };

            return this.http.post<any>(authUrl, body, httpOptions)
                .pipe(
                    map((token: string) => {
                        this.parseJwtToken(token);
                        this.router.navigate(["apps", "chat"]);
                    })
                )
        }
    }

    logInGoogle(googleAccessToken: string) {


        return this.http.post<string>(authGoogleUrl + `?accessToken=${googleAccessToken}`, null, httpOptions).pipe(
            map((token: string) => { 
                this.parseJwtToken(token);
                this.router.navigate(["apps", "chat"]);
            })
        ).subscribe();
    }

    logInFacebook(facebookAccessToken:string) {
        return this.http.post<string>(authFBUrl + `?token=${facebookAccessToken}`, null, httpOptions)
        .pipe(
            map((token: string) => {
                this.parseJwtToken(token);
                this.router.navigate(['apps','chat']);
            })
        ).subscribe()
    }

    logOut(): Promise<boolean> {
        this.authService.clearToken();
        return this.router.navigate(['account', 'login']);
    }

    public parseJwtToken(token: string) {
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
        console.log(this.userProfile.DisplayName)
    }

    loadStoredUserProfile() {
        let token = this.authService.getToken();
        if (token.length > 0) {
            this.parseJwtToken(token);
        }
    }
}
