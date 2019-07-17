import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
let FuseSidebarService = class FuseSidebarService {
    /**
     * Constructor
     */
    constructor() {
        // Private
        this._registry = {};
    }
    /**
     * Add the sidebar to the registry
     *
     * @param key
     * @param sidebar
     */
    register(key, sidebar) {
        // Check if the key already being used
        if (this._registry[key]) {
            console.error(`The sidebar with the key '${key}' already exists. Either unregister it first or use a unique key.`);
            return;
        }
        // Add to the registry
        this._registry[key] = sidebar;
    }
    /**
     * Remove the sidebar from the registry
     *
     * @param key
     */
    unregister(key) {
        // Check if the sidebar exists
        if (!this._registry[key]) {
            console.warn(`The sidebar with the key '${key}' doesn't exist in the registry.`);
        }
        // Unregister the sidebar
        delete this._registry[key];
    }
    /**
     * Return the sidebar with the given key
     *
     * @param key
     * @returns {FuseSidebarComponent}
     */
    getSidebar(key) {
        // Check if the sidebar exists
        if (!this._registry[key]) {
            console.warn(`The sidebar with the key '${key}' doesn't exist in the registry.`);
            return;
        }
        // Return the sidebar
        return this._registry[key];
    }
};
FuseSidebarService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    }),
    tslib_1.__metadata("design:paramtypes", [])
], FuseSidebarService);
export { FuseSidebarService };
//# sourceMappingURL=sidebar.service.js.map