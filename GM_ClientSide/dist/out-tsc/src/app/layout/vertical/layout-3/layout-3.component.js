import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseConfigService } from '@fuse/services/config.service';
import { navigation } from 'app/navigation/navigation';
let VerticalLayout3Component = class VerticalLayout3Component {
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     */
    constructor(_fuseConfigService) {
        this._fuseConfigService = _fuseConfigService;
        // Set the defaults
        this.navigation = navigation;
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------
    /**
     * On init
     */
    ngOnInit() {
        // Subscribe to config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
            this.fuseConfig = config;
        });
    }
    /**
     * On destroy
     */
    ngOnDestroy() {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
};
VerticalLayout3Component = tslib_1.__decorate([
    Component({
        selector: 'vertical-layout-3',
        templateUrl: './layout-3.component.html',
        styleUrls: ['./layout-3.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [FuseConfigService])
], VerticalLayout3Component);
export { VerticalLayout3Component };
//# sourceMappingURL=layout-3.component.js.map