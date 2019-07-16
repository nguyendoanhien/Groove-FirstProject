import { Component, ViewEncapsulation } from '@angular/core';
import { FuseConfigService } from '@fuse/services/config.service';
@Component({
    selector     : 'page-not-found',
    templateUrl  : './page-not-found.component.html',
    styleUrls    : ['./page-not-found.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class PageNotFoundComponent
{
    /**
     * Constructor
     */
    constructor(private _fuseConfigService: FuseConfigService)
    {
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
