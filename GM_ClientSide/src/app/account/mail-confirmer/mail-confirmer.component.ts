import { Component, ViewEncapsulation, OnInit } from '@angular/core';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { RegisterService } from 'app/core/account/register.service';
import { Router, ActivatedRoute } from "@angular/router"
import { MailConfirmModel } from './mail-confirmer.model';

import { Route } from '@angular/compiler/src/core';
import { UserProfileService } from 'app/core/identity/userprofile.service';
@Component({
    selector: 'mail-confirmer',
    templateUrl: './mail-confirmer.component.html',
    styleUrls: ['./mail-confirmer.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class MailConfirmerComponent implements OnInit {
    ctoken: string;
    userid: string;
    isLoading: boolean;
    isFailure:boolean=false;
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {RegisterService} _registerService
     * @param {Router} _router
     * @param {UserProfileService} _userProfileService
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _registerService: RegisterService,
        private _route: ActivatedRoute,
        private _router: Router,
        private _userProfileService: UserProfileService,
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
    async ngOnInit() {
        this.isLoading = true;
        var model = await this.getParams();
        this._registerService.confirmEmail(model).subscribe(async token => {
            this.isLoading = false;
            await this._userProfileService.parseJwtToken(token);
            this._router.navigate(['chat']);
        }, fail => {
            this.isLoading = false;
            this.isFailure=true;
            
        });
    }
    getParams(): MailConfirmModel {
        var model: MailConfirmModel = new MailConfirmModel();
        var urlOrigin = window.location.href;
        var regexp = new RegExp(/[^&?]*?=[^&?]*/g);
        var matches: any;
        var values = [];
        while (matches = regexp.exec(urlOrigin)) {
            values.push(matches[0]);
        }

        var regexUserId = new RegExp(/(?<=userid=).*$/);
        var regexCtoken = new RegExp(/(?<=ctoken=).*$/);

        model.ctoken = regexCtoken.exec(values[0]).toString();
        model.userId = regexUserId.exec(values[1]).toString();
        return model;
    }
}
