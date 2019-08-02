import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from "@angular/forms";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { Router } from "@angular/router"
import { FuseConfigService } from "@fuse/services/config.service";
import { fuseAnimations } from "@fuse/animations";
import { RegisterModel } from "./register.model";
import { RegisterService } from "app/core/account/register.service";

@Component({
    selector: "register",
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class RegisterComponent implements OnInit, OnDestroy {
    registerForm: FormGroup;
    isAcceptTerms = false;
    isLoading = false;
    registerModel: RegisterModel;
    emailErr = false;
    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _router: Router,
        private registerService: RegisterService) {
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

    /**
     * On click events
     */
    onCreateAnAccount() {
        this.emailErr = false;
        this.isLoading = true;
        this.registerService.register(this.registerModel).subscribe(res => {
                this.isLoading = false;
                this._router.navigate(["/account/mail-confirmation"]);
                this.registerService.mailToSendRegister.next(this.registerModel.email);
            },
            err => {
                this.isLoading = false;

                this.emailErr = !err.error.errors["Email"] ? false : true;
            });
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.registerModel = new RegisterModel();
        this.registerForm = this._formBuilder.group({
            name: [
                this.registerModel.displayName,
                [
                    Validators.required, Validators.minLength(6), Validators.maxLength(120),
                    Validators.pattern(/^((?!\s{2,}).)*$/)
                ]
            ],
            email: [
                this.registerModel.email,
                [Validators.required, Validators.pattern(/^\s*(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))\s*$/)]
            ],
            password: [
                this.registerModel.password,
                [
                    Validators.required,
                    Validators.pattern(/^(?=[a-zA-Z0-9!%^&*()+#@$?]{8,40}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$/)
                ]
            ],
            passwordConfirm: ["", [Validators.required, confirmPasswordValidator]]
        });
        // Update the validity of the 'passwordConfirm' field
        // when the 'password' field changes
        this.registerForm.get("password").valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
                this.registerForm.get("passwordConfirm").updateValueAndValidity();
            });
    }

    onPaste(event: ClipboardEvent) {
        event.preventDefault();
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
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

    const password = control.parent.get("password");
    const passwordConfirm = control.parent.get("passwordConfirm");

    if (!password || !passwordConfirm) {
        return null;
    }

    if (passwordConfirm.value === "") {
        return null;
    }

    if (password.value === passwordConfirm.value) {
        return null;
    }

    return { passwordsNotMatching: true };
};