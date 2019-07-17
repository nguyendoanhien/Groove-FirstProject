import * as tslib_1 from "tslib";
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { ResolveEnd, Router } from '@angular/router';
import { Platform } from '@angular/cdk/platform';
import { BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';
import * as _ from 'lodash';
// Create the injection token for the custom settings
export const FUSE_CONFIG = new InjectionToken('fuseCustomConfig');
let FuseConfigService = class FuseConfigService {
    /**
     * Constructor
     *
     * @param {Platform} _platform
     * @param {Router} _router
     * @param _config
     */
    constructor(_platform, _router, _config) {
        this._platform = _platform;
        this._router = _router;
        this._config = _config;
        // Set the default config from the user provided config (from forRoot)
        this._defaultConfig = _config;
        // Initialize the service
        this._init();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------
    /**
     * Set and get the config
     */
    set config(value) {
        // Get the value from the behavior subject
        let config = this._configSubject.getValue();
        // Merge the new config
        config = _.merge({}, config, value);
        // Notify the observers
        this._configSubject.next(config);
    }
    get config() {
        return this._configSubject.asObservable();
    }
    /**
     * Get default config
     *
     * @returns {any}
     */
    get defaultConfig() {
        return this._defaultConfig;
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
        /**
         * Disable custom scrollbars if browser is mobile
         */
        if (this._platform.ANDROID || this._platform.IOS) {
            this._defaultConfig.customScrollbars = false;
        }
        // Set the config from the default config
        this._configSubject = new BehaviorSubject(_.cloneDeep(this._defaultConfig));
        // Reload the default layout config on every RoutesRecognized event
        // if the current layout config is different from the default one
        this._router.events
            .pipe(filter(event => event instanceof ResolveEnd))
            .subscribe(() => {
            if (!_.isEqual(this._configSubject.getValue().layout, this._defaultConfig.layout)) {
                // Clone the current config
                const config = _.cloneDeep(this._configSubject.getValue());
                // Reset the layout from the default config
                config.layout = _.cloneDeep(this._defaultConfig.layout);
                // Set the config
                this._configSubject.next(config);
            }
        });
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Set config
     *
     * @param value
     * @param {{emitEvent: boolean}} opts
     */
    setConfig(value, opts = { emitEvent: true }) {
        // Get the value from the behavior subject
        let config = this._configSubject.getValue();
        // Merge the new config
        config = _.merge({}, config, value);
        // If emitEvent option is true...
        if (opts.emitEvent === true) {
            // Notify the observers
            this._configSubject.next(config);
        }
    }
    /**
     * Get config
     *
     * @returns {Observable<any>}
     */
    getConfig() {
        return this._configSubject.asObservable();
    }
    /**
     * Reset to the default config
     */
    resetToDefaults() {
        // Set the config from the default config
        this._configSubject.next(_.cloneDeep(this._defaultConfig));
    }
};
FuseConfigService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    }),
    tslib_1.__param(2, Inject(FUSE_CONFIG)),
    tslib_1.__metadata("design:paramtypes", [Platform,
        Router, Object])
], FuseConfigService);
export { FuseConfigService };
//# sourceMappingURL=config.service.js.map