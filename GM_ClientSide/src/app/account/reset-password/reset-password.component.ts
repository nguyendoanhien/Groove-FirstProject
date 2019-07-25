import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/internal/operators';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { ResetPasswordService } from 'app/core/account/reset-password.service';
import { ResetPassword } from '../../models/ResetPassword';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
    selector: 'reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ResetPasswordComponent implements OnInit, OnDestroy {
    resetPasswordForm: FormGroup;
    token: string;
    userid: string;
    isResetFailture:boolean = false;
    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _resetPasswordService: ResetPasswordService,
        private _route: Router,
        private _router: ActivatedRoute
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

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.token = this._router.snapshot.queryParamMap.get('token');
        this.userid = this._router.snapshot.queryParamMap.get('userid');
        this.resetPasswordForm = this._formBuilder.group({
            email: ['', [Validators.required, Validators.pattern(/^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{3,}(\.[a-z0-9]{2,4}){1,2}$/)]],
            password: ['', [
                Validators.required,
                Validators.minLength(8),
                Validators.maxLength(40),
                Validators.pattern(/^(?=[a-zA-Z0-9!%^&*()+#@$?]{8,40}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$/)
            ]],
            passwordConfirm: ['', [Validators.required, confirmPasswordValidator]]
        });

        // Update the validity of the 'passwordConfirm' field
        // when the 'password' field changes
        this.resetPasswordForm.get('password').valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
                this.resetPasswordForm.get('passwordConfirm').updateValueAndValidity();
            });

        // Add 

    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
    /**
     * On destroy
     */
    onSubmit(): void {
        const resetPassword: ResetPassword = {
            email: this.resetPasswordForm.get('email').value,
            userid: this.userid,
            ctoken: this.token,
            newpassword: this.resetPasswordForm.get('password').value,
        };        
        this._resetPasswordService.resetPassword(resetPassword).subscribe(val => {
            this._route.navigate(['/account/login']);
        }, err =>this.isResetFailture=true);

    }
}

/**
 * Confirm password validator
 *
 * @param {AbstractControl} control
 * @returns {ValidationErrors | null}
 */
export const confirmPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

    if (!control.parent || !control) {
        return null;
    }

    const password = control.parent.get('password');
    const passwordConfirm = control.parent.get('passwordConfirm');

    if (!password || !passwordConfirm) {
        return null;
    }

    if (passwordConfirm.value === '') {
        return null;
    }

    if (password.value === passwordConfirm.value) {
        return null;
    }

    return { passwordsNotMatching: true };
};
