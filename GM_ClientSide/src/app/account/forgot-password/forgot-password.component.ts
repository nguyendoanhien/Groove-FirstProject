import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { ResetPasswordService } from 'app/services/reset-password.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'forgot-password',
    templateUrl: './forgot-password.component.html',
    styleUrls: ['./forgot-password.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ForgotPasswordComponent implements OnInit {
    forgotPasswordForm: FormGroup;
    isLoading:boolean= false;
    isEmailAlreadyExist:boolean = true;
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
            email: ['', [Validators.required, Validators.email]]
        });
    }

    onSubmit(): void {
        this.isLoading = true;
        const email = this.forgotPasswordForm.get('email').value;
        this._resetPasswordService.forgotPassword(email).subscribe(val => {
            this.isLoading = false;
            this._route.navigate(['/account/login']);
        }, err => {
            this.isLoading = false;
            this.isEmailAlreadyExist = false;
        });
    }
}
