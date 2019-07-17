import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
let FuseMatSidenavHelperService = class FuseMatSidenavHelperService {
    /**
     * Constructor
     */
    constructor() {
        this.sidenavInstances = [];
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------
    /**
     * Set sidenav
     *
     * @param id
     * @param instance
     */
    setSidenav(id, instance) {
        this.sidenavInstances[id] = instance;
    }
    /**
     * Get sidenav
     *
     * @param id
     * @returns {any}
     */
    getSidenav(id) {
        return this.sidenavInstances[id];
    }
};
FuseMatSidenavHelperService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    }),
    tslib_1.__metadata("design:paramtypes", [])
], FuseMatSidenavHelperService);
export { FuseMatSidenavHelperService };
//# sourceMappingURL=fuse-mat-sidenav.service.js.map