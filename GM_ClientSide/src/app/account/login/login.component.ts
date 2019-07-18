import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { UserProfileService } from 'app/core/identity/userprofile.service';
import { Router } from '@angular/router';
import { AuthService, GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    checkRemember: Boolean = false;
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _userProfileService: UserProfileService,
        private _cookieService: CookieService,
        private _authService: AuthService,
        private _router:Router
    ) {
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
    ngOnInit(): void {
        if (this._cookieService.get('userName')) {
            this.checkRemember = true;
            this.loginForm = this._formBuilder.group({
                userName: [this._cookieService.get('userName'), [Validators.required, Validators.pattern("[a-zA-Z0-9.-]{1,}@[a-z0-9.-]+\.[a-z]{2,4}$"),Validators.minLength(6)]],
                password: [this._cookieService.get('password'), [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/)]]
            });
        } else {
            this.loginForm = this._formBuilder.group({
                userName: ['', [Validators.required, Validators.pattern("[a-zA-Z0-9.-]{1,}@[a-z0-9.-]+\.[a-z]{2,4}$"),Validators.minLength(6)]],
                password: ['', [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/)]]
            });
        }

        if(localStorage.getItem('token')!=null)
        {
            this._router.navigate(['apps','chat']);
        }
    }

    onPaste(event: ClipboardEvent) {
        event.preventDefault()
     }

    onLogin() {

        this._userProfileService.logIn(this.loginForm.value).subscribe(res => { this.rememberLogin() }, err => alert(err.error));
    }

    rememberLogin() {
        if (this.checkRemember) {
            this._cookieService.set('userName', this.loginForm.value.userName);
            this._cookieService.set('password', this.loginForm.value.password);

        } else {
            this._cookieService.deleteAll()

        }
    }

    signinWithGoogle(): void {

        const socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;

        this._authService.signIn(socialPlatformProvider)
            .then((userData) => {

                
                this._userProfileService.logInGoogle(userData.idToken);

            });
    }

    signinWithFB(): void {
        this._authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(userData => {
            this._userProfileService.logInFacebook(userData.authToken);

        });
    }


}

// root123@gmail.com  Root1234