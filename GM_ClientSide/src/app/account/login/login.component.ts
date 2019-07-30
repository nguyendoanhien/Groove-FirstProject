import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CookieService } from "ngx-cookie-service";
import { from } from "rxjs";
import { FuseConfigService } from "@fuse/services/config.service";
import { fuseAnimations } from "@fuse/animations";
import { UserProfileService } from "app/core/identity/userprofile.service";
import { Router } from "@angular/router";
import { AuthService, GoogleLoginProvider, FacebookLoginProvider } from "angularx-social-login";
import { interval } from "rxjs";
import { FacebookService } from 'ngx-facebook';


@Component({
    selector: "login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    checkRemember = false;

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
        private _router: Router,
        private _fb: FacebookService
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
        // this._fb.init({
        //     appId: '354060818601401',
        //     cookie: false,
        //     // autoLogAppEvents: true,
        //     xfbml: true,
        //     version: 'v3.3'
        // });
        if (this._cookieService.get("userName")) {
            this.checkRemember = true;
            this.loginForm = this._formBuilder.group({
                userName: [
                    this._cookieService.get("userName"),
                    [
                        Validators.required,
                        Validators.pattern(/^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{3,}(\.[a-z0-9]{2,4}){1,2}$/),
                        Validators.minLength(6)
                    ]
                ],
                password: [
                    this._cookieService.get("password"),
                    [
                        Validators.required,
                        Validators.pattern(/^(?=[a-zA-Z0-9!%^&*()+#@$?]{8,40}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$/)
                    ]
                ]
            });
        } else {
            this.loginForm = this._formBuilder.group({
                userName: [
                    "",
                    [
                        Validators.required,
                        Validators.pattern(/^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{3,}(\.[a-z0-9]{2,4}){1,2}$/),
                        Validators.minLength(6)
                    ]
                ],
                password: [
                    "",
                    [
                        Validators.required,
                        Validators.pattern(/^(?=[a-zA-Z0-9!%^&*()+#@$?]{8,40}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$/)
                    ]
                ]
            });
        }

        if (localStorage.getItem("token") != null) {
            this._router.navigate(["apps", "chat"]);
        }
    }

    onPaste(event: ClipboardEvent) {
        event.preventDefault();
    }

    onLogin() {

        this._userProfileService.logIn(this.loginForm.value)
            .subscribe(res => { this.rememberLogin(); }, err => alert(err.error));
    }

    rememberLogin() {
        if (this.checkRemember) {
            this._cookieService.set("userName", this.loginForm.value.userName);
            this._cookieService.set("password", this.loginForm.value.password);

        } else {
            this._cookieService.deleteAll();

        }
    }

    signinWithGoogle(): void {
        const socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;

        from(this._authService.signIn(socialPlatformProvider))
            .subscribe((userData) => {


                this._userProfileService.logInGoogle(userData.idToken);

            },
                (err) => console.log("Error!"));
    }

    signinWithFB(): void {
        this._fb.login().then
            ((userData) => {

                this._userProfileService.logInFacebook(this._fb.getAuthResponse()['accessToken']);
            }
            )
    }


}