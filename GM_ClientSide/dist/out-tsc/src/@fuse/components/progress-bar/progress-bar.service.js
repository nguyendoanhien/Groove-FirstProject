import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';
let FuseProgressBarService = class FuseProgressBarService {
    /**
     * Constructor
     *
     * @param {Router} _router
     */
    constructor(_router) {
        this._router = _router;
        // Initialize the service
        this._init();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------
    /**
     * Buffer value
     */
    get bufferValue() {
        return this._bufferValue.asObservable();
    }
    setBufferValue(value) {
        this._bufferValue.next(value);
    }
    /**
     * Mode
     */
    get mode() {
        return this._mode.asObservable();
    }
    setMode(value) {
        this._mode.next(value);
    }
    /**
     * Value
     */
    get value() {
        return this._value.asObservable();
    }
    setValue(value) {
        this._value.next(value);
    }
    /**
     * Visible
     */
    get visible() {
        return this._visible.asObservable();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Initialize
     *
     * @private
     */
    _init() {
        // Initialize the behavior subjects
        this._bufferValue = new BehaviorSubject(0);
        this._mode = new BehaviorSubject('indeterminate');
        this._value = new BehaviorSubject(0);
        this._visible = new BehaviorSubject(false);
        // Subscribe to the router events to show/hide the loading bar
        this._router.events
            .pipe(filter((event) => event instanceof NavigationStart))
            .subscribe(() => {
            this.show();
        });
        this._router.events
            .pipe(filter((event) => event instanceof NavigationEnd || event instanceof NavigationError || event instanceof NavigationCancel))
            .subscribe(() => {
            this.hide();
        });
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Show the progress bar
     */
    show() {
        this._visible.next(true);
    }
    /**
     * Hide the progress bar
     */
    hide() {
        this._visible.next(false);
    }
};
FuseProgressBarService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    }),
    tslib_1.__metadata("design:paramtypes", [Router])
], FuseProgressBarService);
export { FuseProgressBarService };
//# sourceMappingURL=progress-bar.service.js.map