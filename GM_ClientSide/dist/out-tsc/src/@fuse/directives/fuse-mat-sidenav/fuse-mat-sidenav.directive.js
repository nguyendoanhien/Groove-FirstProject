import * as tslib_1 from "tslib";
import { Directive, Input, HostListener, HostBinding } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { MediaObserver } from '@angular/flex-layout';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseMatchMediaService } from '@fuse/services/match-media.service';
import { FuseMatSidenavHelperService } from '@fuse/directives/fuse-mat-sidenav/fuse-mat-sidenav.service';
let FuseMatSidenavHelperDirective = class FuseMatSidenavHelperDirective {
    /**
     * Constructor
     *
     * @param {FuseMatchMediaService} _fuseMatchMediaService
     * @param {FuseMatSidenavHelperService} _fuseMatSidenavHelperService
     * @param {MatSidenav} _matSidenav
     * @param {MediaObserver} _mediaObserver
     */
    constructor(_fuseMatchMediaService, _fuseMatSidenavHelperService, _matSidenav, _mediaObserver) {
        this._fuseMatchMediaService = _fuseMatchMediaService;
        this._fuseMatSidenavHelperService = _fuseMatSidenavHelperService;
        this._matSidenav = _matSidenav;
        this._mediaObserver = _mediaObserver;
        // Set the defaults
        this.isLockedOpen = true;
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
        // Register the sidenav to the service
        this._fuseMatSidenavHelperService.setSidenav(this.fuseMatSidenavHelper, this._matSidenav);
        if (this.matIsLockedOpen && this._mediaObserver.isActive(this.matIsLockedOpen)) {
            this.isLockedOpen = true;
            this._matSidenav.mode = 'side';
            this._matSidenav.toggle(true);
        }
        else {
            this.isLockedOpen = false;
            this._matSidenav.mode = 'over';
            this._matSidenav.toggle(false);
        }
        this._fuseMatchMediaService.onMediaChange
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
            if (this.matIsLockedOpen && this._mediaObserver.isActive(this.matIsLockedOpen)) {
                this.isLockedOpen = true;
                this._matSidenav.mode = 'side';
                this._matSidenav.toggle(true);
            }
            else {
                this.isLockedOpen = false;
                this._matSidenav.mode = 'over';
                this._matSidenav.toggle(false);
            }
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
tslib_1.__decorate([
    HostBinding('class.mat-is-locked-open'),
    tslib_1.__metadata("design:type", Boolean)
], FuseMatSidenavHelperDirective.prototype, "isLockedOpen", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String)
], FuseMatSidenavHelperDirective.prototype, "fuseMatSidenavHelper", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String)
], FuseMatSidenavHelperDirective.prototype, "matIsLockedOpen", void 0);
FuseMatSidenavHelperDirective = tslib_1.__decorate([
    Directive({
        selector: '[fuseMatSidenavHelper]'
    }),
    tslib_1.__metadata("design:paramtypes", [FuseMatchMediaService,
        FuseMatSidenavHelperService,
        MatSidenav,
        MediaObserver])
], FuseMatSidenavHelperDirective);
export { FuseMatSidenavHelperDirective };
let FuseMatSidenavTogglerDirective = class FuseMatSidenavTogglerDirective {
    /**
     * Constructor
     *
     * @param {FuseMatSidenavHelperService} _fuseMatSidenavHelperService
     */
    constructor(_fuseMatSidenavHelperService) {
        this._fuseMatSidenavHelperService = _fuseMatSidenavHelperService;
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * On click
     */
    onClick() {
        this._fuseMatSidenavHelperService.getSidenav(this.fuseMatSidenavToggler).toggle();
    }
};
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String)
], FuseMatSidenavTogglerDirective.prototype, "fuseMatSidenavToggler", void 0);
tslib_1.__decorate([
    HostListener('click'),
    tslib_1.__metadata("design:type", Function),
    tslib_1.__metadata("design:paramtypes", []),
    tslib_1.__metadata("design:returntype", void 0)
], FuseMatSidenavTogglerDirective.prototype, "onClick", null);
FuseMatSidenavTogglerDirective = tslib_1.__decorate([
    Directive({
        selector: '[fuseMatSidenavToggler]'
    }),
    tslib_1.__metadata("design:paramtypes", [FuseMatSidenavHelperService])
], FuseMatSidenavTogglerDirective);
export { FuseMatSidenavTogglerDirective };
//# sourceMappingURL=fuse-mat-sidenav.directive.js.map