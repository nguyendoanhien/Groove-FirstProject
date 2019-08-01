import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { fuseAnimations } from "@fuse/animations";
import { FuseConfigService } from "@fuse/services/config.service";
import { ResetPasswordService } from "app/core/account/reset-password.service";


@Component({
    selector: "forgot-password",
    templateUrl: "./forgot-password.component.html",
    styleUrls: ["./forgot-password.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ForgotPasswordComponent implements OnInit {
    forgotPasswordForm: FormGroup;
    isLoading = false;
    isEmailAlreadyExist = true;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _resetPasswordService: ResetPasswordService,
        private _route: Router,
        private _router: ActivatedRoute,
        private _formBuilder: FormBuilder
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
        this.forgotPasswordForm = this._formBuilder.group({
            email: [
                "",
                [
                    Validators.required,
                    Validators.pattern(/^\s*(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))\s*$/),
                    Validators.minLength(6)
                ]
            ]
        });
    }

    onSubmit(): void {
        this.isLoading = true;
        const email = this.forgotPasswordForm.get("email").value;
        this._resetPasswordService.forgotPassword(email).subscribe(val => {
                this.isLoading = false;
                this._resetPasswordService.mailToSendForgot.next(email);
                this._route.navigate(["/account/mail-confirmation"]);
            },
            err => {
                this.isLoading = false;
                this.isEmailAlreadyExist = false;
            });
    }
}