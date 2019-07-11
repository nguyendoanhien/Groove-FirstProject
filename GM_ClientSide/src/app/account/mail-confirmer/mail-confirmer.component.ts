import { Component, ViewEncapsulation, OnInit } from '@angular/core';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { RegisterService } from 'app/core/identity/register.service';
import { Router, ActivatedRoute } from "@angular/router"
import { MailConfirmModel } from './mail-confirmer.model';
import { Route } from '@angular/compiler/src/core';
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
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {RegisterService} _registerService
     * @param {Router} _router
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _registerService: RegisterService,
        private _route: ActivatedRoute,
        private _router: Router
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
        this._registerService.confirmEmail(model).subscribe(sussess => {
            this.isLoading = false;
            this._router.navigate(['/apps/chat']);
        }, fail => {
            this.isLoading = false;
            console.log(fail);
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
        console.log(values);
        var regexUserId = new RegExp(/(?<=userid=).*$/);
        var regexCtoken = new RegExp(/(?<=ctoken=).*$/);

        model.ctoken = regexCtoken.exec(values[0]).toString();
        model.userId = regexUserId.exec(values[1]).toString();
        return model;
    }
}
