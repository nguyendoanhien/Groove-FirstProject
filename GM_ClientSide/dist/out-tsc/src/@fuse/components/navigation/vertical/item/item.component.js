import * as tslib_1 from "tslib";
import { ChangeDetectorRef, Component, HostBinding, Input } from '@angular/core';
import { merge, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
let FuseNavVerticalItemComponent = class FuseNavVerticalItemComponent {
    /**
     * Constructor
     */
    /**
     *
     * @param {ChangeDetectorRef} _changeDetectorRef
     * @param {FuseNavigationService} _fuseNavigationService
     */
    constructor(_changeDetectorRef, _fuseNavigationService) {
        this._changeDetectorRef = _changeDetectorRef;
        this._fuseNavigationService = _fuseNavigationService;
        this.classes = 'nav-item';
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
        // Subscribe to navigation item
        merge(this._fuseNavigationService.onNavigationItemAdded, this._fuseNavigationService.onNavigationItemUpdated, this._fuseNavigationService.onNavigationItemRemoved).pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
            // Mark for check
            this._changeDetectorRef.markForCheck();
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
    HostBinding('class'),
    tslib_1.__metadata("design:type", Object)
], FuseNavVerticalItemComponent.prototype, "classes", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", Object)
], FuseNavVerticalItemComponent.prototype, "item", void 0);
FuseNavVerticalItemComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-nav-vertical-item',
        templateUrl: './item.component.html',
        styleUrls: ['./item.component.scss']
    }),
    tslib_1.__metadata("design:paramtypes", [ChangeDetectorRef,
        FuseNavigationService])
], FuseNavVerticalItemComponent);
export { FuseNavVerticalItemComponent };
//# sourceMappingURL=item.component.js.map