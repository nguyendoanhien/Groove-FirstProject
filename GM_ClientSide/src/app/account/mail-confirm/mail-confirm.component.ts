import { Component, ViewEncapsulation } from '@angular/core';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { RegisterService } from 'app/core/account/register.service';
import { Router } from "@angular/router"
import { ResetPasswordService } from 'app/core/account/reset-password.service';
@Component({
    selector: 'mail-confirm',
    templateUrl: './mail-confirm.component.html',
    styleUrls: ['./mail-confirm.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class MailConfirmComponent {
    mailToSendForgot: string;
    mailToSendRegister: boolean;
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {RegisterService} _registerService
     * @param {Router} _router
     * @param {ResetPasswordService} _resetPassService
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _registerService: RegisterService,
        private _router: Router,
        private _resetPassService: ResetPasswordService
    ) {
        this._registerService.mailToSendRegister.subscribe(emailAddress => {
            emailAddress ? this.mailToSendRegister = emailAddress : this._resetPassService.mailToSendForgot.subscribe(
                emailAddress => emailAddress ? this.mailToSendForgot = emailAddress : this._router.navigate(['account', 'login'])
            )
        });
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
}
