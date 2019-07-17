import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
let FuseProgressBarComponent = class FuseProgressBarComponent {
    /**
     * Constructor
     *
     * @param {FuseProgressBarService} _fuseProgressBarService
     */
    constructor(_fuseProgressBarService) {
        // Set the defaults
        this._fuseProgressBarService = _fuseProgressBarService;
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
        // Subscribe to the progress bar service properties
        // Buffer value
        this._fuseProgressBarService.bufferValue
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((bufferValue) => {
            this.bufferValue = bufferValue;
        });
        // Mode
        this._fuseProgressBarService.mode
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((mode) => {
            this.mode = mode;
        });
        // Value
        this._fuseProgressBarService.value
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((value) => {
            this.value = value;
        });
        // Visible
        this._fuseProgressBarService.visible
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((visible) => {
            this.visible = visible;
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
FuseProgressBarComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-progress-bar',
        templateUrl: './progress-bar.component.html',
        styleUrls: ['./progress-bar.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [FuseProgressBarService])
], FuseProgressBarComponent);
export { FuseProgressBarComponent };
//# sourceMappingURL=progress-bar.component.js.map