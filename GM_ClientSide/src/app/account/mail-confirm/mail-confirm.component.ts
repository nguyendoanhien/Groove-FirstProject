import { Component, ViewEncapsulation } from '@angular/core';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { RegisterService } from 'app/core/account/register.service';
import { Router } from "@angular/router"
@Component({
    selector     : 'mail-confirm',
    templateUrl  : './mail-confirm.component.html',
    styleUrls    : ['./mail-confirm.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class MailConfirmComponent
{
    emailAddress: string;
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
        private _router: Router
    )
    {
        this._registerService.email.subscribe(emailAddress=>emailAddress?this.emailAddress = emailAddress:this._router.navigate(['/account/register']))
        // Configure the layout
        this._fuseConfigService.config = {
            layout: {
                navbar   : {
                    hidden: true
                },
                toolbar  : {
                    hidden: true
                },
                footer   : {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };
    }
}
