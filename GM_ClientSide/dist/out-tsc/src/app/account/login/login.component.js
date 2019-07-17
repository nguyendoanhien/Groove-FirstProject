import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { UserProfileService } from 'app/core/identity/userprofile.service';
import { AuthService, GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
let LoginComponent = class LoginComponent {
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(_fuseConfigService, _formBuilder, _userProfileService, _cookieService, _authService) {
        this._fuseConfigService = _fuseConfigService;
        this._formBuilder = _formBuilder;
        this._userProfileService = _userProfileService;
        this._cookieService = _cookieService;
        this._authService = _authService;
        this.checkRemember = false;
        // Configure the layout
        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true
                },
                toolbar: {
                    hidden: true
                },
                footer: {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------
    /**
     * On init
     */
    ngOnInit() {
        if (this._cookieService.get('userName')) {
            this.checkRemember = true;
            this.loginForm = this._formBuilder.group({
                userName: [this._cookieService.get('userName'), [Validators.required, Validators.email]],
                password: [this._cookieService.get('password'), [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/)]]
            });
        }
        else {
            this.loginForm = this._formBuilder.group({
                userName: ['', [Validators.required, Validators.email]],
                password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/)]]
            });
        }
        // if(localStorage.getItem('token')!=null)
        // {
        //     this.router.navigate(['apps','chat']);
        // }
    }
    onLogin() {
        this._userProfileService.logIn(this.loginForm.value).subscribe(res => { this.rememberLogin(); }, err => alert(err.error));
    }
    rememberLogin() {
        if (this.checkRemember) {
            this._cookieService.set('userName', this.loginForm.value.userName);
            this._cookieService.set('password', this.loginForm.value.password);
        }
        else {
            this._cookieService.deleteAll();
        }
    }
    signinWithGoogle() {
        const socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
        this._authService.signIn(socialPlatformProvider)
            .then((userData) => {
            this._userProfileService.logInGoogle(userData.idToken);
        });
    }
    signinWithFB() {
        this._authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(userData => {
            this._userProfileService.logInFacebook(userData.authToken);
        });
    }
};
LoginComponent = tslib_1.__decorate([
    Component({
        selector: 'login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.scss'],
        encapsulation: ViewEncapsulation.None,
        animations: fuseAnimations
    }),
    tslib_1.__metadata("design:paramtypes", [FuseConfigService,
        FormBuilder,
        UserProfileService,
        CookieService,
        AuthService])
], LoginComponent);
export { LoginComponent };
// root123@gmail.com  Root1234
//# sourceMappingURL=login.component.js.map