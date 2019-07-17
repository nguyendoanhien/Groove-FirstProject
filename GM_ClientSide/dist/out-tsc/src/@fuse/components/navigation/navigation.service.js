import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import * as _ from 'lodash';
let FuseNavigationService = class FuseNavigationService {
    /**
     * Constructor
     */
    constructor() {
        this._registry = {};
        // Set the defaults
        this.onItemCollapsed = new Subject();
        this.onItemCollapseToggled = new Subject();
        // Set the private defaults
        this._currentNavigationKey = null;
        this._onNavigationChanged = new BehaviorSubject(null);
        this._onNavigationRegistered = new BehaviorSubject(null);
        this._onNavigationUnregistered = new BehaviorSubject(null);
        this._onNavigationItemAdded = new BehaviorSubject(null);
        this._onNavigationItemUpdated = new BehaviorSubject(null);
        this._onNavigationItemRemoved = new BehaviorSubject(null);
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------
    /**
     * Get onNavigationChanged
     *
     * @returns {Observable<any>}
     */
    get onNavigationChanged() {
        return this._onNavigationChanged.asObservable();
    }
    /**
     * Get onNavigationRegistered
     *
     * @returns {Observable<any>}
     */
    get onNavigationRegistered() {
        return this._onNavigationRegistered.asObservable();
    }
    /**
     * Get onNavigationUnregistered
     *
     * @returns {Observable<any>}
     */
    get onNavigationUnregistered() {
        return this._onNavigationUnregistered.asObservable();
    }
    /**
     * Get onNavigationItemAdded
     *
     * @returns {Observable<any>}
     */
    get onNavigationItemAdded() {
        return this._onNavigationItemAdded.asObservable();
    }
    /**
     * Get onNavigationItemUpdated
     *
     * @returns {Observable<any>}
     */
    get onNavigationItemUpdated() {
        return this._onNavigationItemUpdated.asObservable();
    }
    /**
     * Get onNavigationItemRemoved
     *
     * @returns {Observable<any>}
     */
    get onNavigationItemRemoved() {
        return this._onNavigationItemRemoved.asObservable();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Register the given navigation
     * with the given key
     *
     * @param key
     * @param navigation
     */
    register(key, navigation) {
        // Check if the key already being used
        if (this._registry[key]) {
            console.error(`The navigation with the key '${key}' already exists. Either unregister it first or use a unique key.`);
            return;
        }
        // Add to the registry
        this._registry[key] = navigation;
        // Notify the subject
        this._onNavigationRegistered.next([key, navigation]);
    }
    /**
     * Unregister the navigation from the registry
     * @param key
     */
    unregister(key) {
        // Check if the navigation exists
        if (!this._registry[key]) {
            console.warn(`The navigation with the key '${key}' doesn't exist in the registry.`);
        }
        // Unregister the sidebar
        delete this._registry[key];
        // Notify the subject
        this._onNavigationUnregistered.next(key);
    }
    /**
     * Get navigation from registry by key
     *
     * @param key
     * @returns {any}
     */
    getNavigation(key) {
        // Check if the navigation exists
        if (!this._registry[key]) {
            console.warn(`The navigation with the key '${key}' doesn't exist in the registry.`);
            return;
        }
        // Return the sidebar
        return this._registry[key];
    }
    /**
     * Get flattened navigation array
     *
     * @param navigation
     * @param flatNavigation
     * @returns {any[]}
     */
    getFlatNavigation(navigation, flatNavigation = []) {
        for (const item of navigation) {
            if (item.type === 'item') {
                flatNavigation.push(item);
                continue;
            }
            if (item.type === 'collapsable' || item.type === 'group') {
                if (item.children) {
                    this.getFlatNavigation(item.children, flatNavigation);
                }
            }
        }
        return flatNavigation;
    }
    /**
     * Get the current navigation
     *
     * @returns {any}
     */
    getCurrentNavigation() {
        if (!this._currentNavigationKey) {
            console.warn(`The current navigation is not set.`);
            return;
        }
        return this.getNavigation(this._currentNavigationKey);
    }
    /**
     * Set the navigation with the key
     * as the current navigation
     *
     * @param key
     */
    setCurrentNavigation(key) {
        // Check if the sidebar exists
        if (!this._registry[key]) {
            console.warn(`The navigation with the key '${key}' doesn't exist in the registry.`);
            return;
        }
        // Set the current navigation key
        this._currentNavigationKey = key;
        // Notify the subject
        this._onNavigationChanged.next(key);
    }
    /**
     * Get navigation item by id from the
     * current navigation
     *
     * @param id
     * @param {any} navigation
     * @returns {any | boolean}
     */
    getNavigationItem(id, navigation = null) {
        if (!navigation) {
            navigation = this.getCurrentNavigation();
        }
        for (const item of navigation) {
            if (item.id === id) {
                return item;
            }
            if (item.children) {
                const childItem = this.getNavigationItem(id, item.children);
                if (childItem) {
                    return childItem;
                }
            }
        }
        return false;
    }
    /**
     * Get the parent of the navigation item
     * with the id
     *
     * @param id
     * @param {any} navigation
     * @param parent
     */
    getNavigationItemParent(id, navigation = null, parent = null) {
        if (!navigation) {
            navigation = this.getCurrentNavigation();
            parent = navigation;
        }
        for (const item of navigation) {
            if (item.id === id) {
                return parent;
            }
            if (item.children) {
                const childItem = this.getNavigationItemParent(id, item.children, item);
                if (childItem) {
                    return childItem;
                }
            }
        }
        return false;
    }
    /**
     * Add a navigation item to the specified location
     *
     * @param item
     * @param id
     */
    addNavigationItem(item, id) {
        // Get the current navigation
        const navigation = this.getCurrentNavigation();
        // Add to the end of the navigation
        if (id === 'end') {
            navigation.push(item);
            // Trigger the observable
            this._onNavigationItemAdded.next(true);
            return;
        }
        // Add to the start of the navigation
        if (id === 'start') {
            navigation.unshift(item);
            // Trigger the observable
            this._onNavigationItemAdded.next(true);
            return;
        }
        // Add it to a specific location
        const parent = this.getNavigationItem(id);
        if (parent) {
            // Check if parent has a children entry,
            // and add it if it doesn't
            if (!parent.children) {
                parent.children = [];
            }
            // Add the item
            parent.children.push(item);
        }
        // Trigger the observable
        this._onNavigationItemAdded.next(true);
    }
    /**
     * Update navigation item with the given id
     *
     * @param id
     * @param properties
     */
    updateNavigationItem(id, properties) {
        // Get the navigation item
        const navigationItem = this.getNavigationItem(id);
        // If there is no navigation with the give id, return
        if (!navigationItem) {
            return;
        }
        // Merge the navigation properties
        _.merge(navigationItem, properties);
        // Trigger the observable
        this._onNavigationItemUpdated.next(true);
    }
    /**
     * Remove navigation item with the given id
     *
     * @param id
     */
    removeNavigationItem(id) {
        const item = this.getNavigationItem(id);
        // Return, if there is not such an item
        if (!item) {
            return;
        }
        // Get the parent of the item
        let parent = this.getNavigationItemParent(id);
        // This check is required because of the first level
        // of the navigation, since the first level is not
        // inside the 'children' array
        parent = parent.children || parent;
        // Remove the item
        parent.splice(parent.indexOf(item), 1);
        // Trigger the observable
        this._onNavigationItemRemoved.next(true);
    }
};
FuseNavigationService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    }),
    tslib_1.__metadata("design:paramtypes", [])
], FuseNavigationService);
export { FuseNavigationService };
//# sourceMappingURL=navigation.service.js.map