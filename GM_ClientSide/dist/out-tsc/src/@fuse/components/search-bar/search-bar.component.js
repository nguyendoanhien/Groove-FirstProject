import * as tslib_1 from "tslib";
import { Component, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseConfigService } from '@fuse/services/config.service';
let FuseSearchBarComponent = class FuseSearchBarComponent {
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     */
    constructor(_fuseConfigService) {
        this._fuseConfigService = _fuseConfigService;
        // Set the defaults
        this.input = new EventEmitter();
        this.collapsed = true;
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
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Collapse
     */
    collapse() {
        this.collapsed = true;
    }
    /**
     * Expand
     */
    expand() {
        this.collapsed = false;
    }
    /**
     * Search
     *
     * @param event
     */
    search(event) {
        this.input.emit(event.target.value);
    }
};
tslib_1.__decorate([
    Output(),
    tslib_1.__metadata("design:type", EventEmitter)
], FuseSearchBarComponent.prototype, "input", void 0);
FuseSearchBarComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-search-bar',
        templateUrl: './search-bar.component.html',
        styleUrls: ['./search-bar.component.scss']
    }),
    tslib_1.__metadata("design:paramtypes", [FuseConfigService])
], FuseSearchBarComponent);
export { FuseSearchBarComponent };
//# sourceMappingURL=search-bar.component.js.map