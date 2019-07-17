import * as tslib_1 from "tslib";
import { MediaObserver } from '@angular/flex-layout';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
let FuseMatchMediaService = class FuseMatchMediaService {
    /**
     * Constructor
     *
     * @param {MediaObserver} _mediaObserver
     */
    constructor(_mediaObserver) {
        this._mediaObserver = _mediaObserver;
        this.onMediaChange = new BehaviorSubject('');
        // Set the defaults
        this.activeMediaQuery = '';
        // Initialize
        this._init();
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
        this._mediaObserver.media$
            .pipe(debounceTime(500), distinctUntilChanged())
            .subscribe((change) => {
            if (this.activeMediaQuery !== change.mqAlias) {
                this.activeMediaQuery = change.mqAlias;
                this.onMediaChange.next(change.mqAlias);
            }
        });
    }
};
FuseMatchMediaService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    }),
    tslib_1.__metadata("design:paramtypes", [MediaObserver])
], FuseMatchMediaService);
export { FuseMatchMediaService };
//# sourceMappingURL=match-media.service.js.map