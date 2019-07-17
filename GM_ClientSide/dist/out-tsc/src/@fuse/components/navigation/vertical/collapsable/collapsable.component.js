import * as tslib_1 from "tslib";
import { ChangeDetectorRef, Component, HostBinding, Input } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { merge, Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
let FuseNavVerticalCollapsableComponent = class FuseNavVerticalCollapsableComponent {
    /**
     * Constructor
     *
     * @param {ChangeDetectorRef} _changeDetectorRef
     * @param {FuseNavigationService} _fuseNavigationService
     * @param {Router} _router
     */
    constructor(_changeDetectorRef, _fuseNavigationService, _router) {
        this._changeDetectorRef = _changeDetectorRef;
        this._fuseNavigationService = _fuseNavigationService;
        this._router = _router;
        this.classes = 'nav-collapsable nav-item';
        this.isOpen = false;
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
        // Listen for router events
        this._router.events
            .pipe(filter(event => event instanceof NavigationEnd), takeUntil(this._unsubscribeAll))
            .subscribe((event) => {
            // Check if the url can be found in
            // one of the children of this item
            if (this.isUrlInChildren(this.item, event.urlAfterRedirects)) {
                this.expand();
            }
            else {
                this.collapse();
            }
        });
        // Listen for collapsing of any navigation item
        this._fuseNavigationService.onItemCollapsed
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((clickedItem) => {
            if (clickedItem && clickedItem.children) {
                // Check if the clicked item is one
                // of the children of this item
                if (this.isChildrenOf(this.item, clickedItem)) {
                    return;
                }
                // Check if the url can be found in
                // one of the children of this item
                if (this.isUrlInChildren(this.item, this._router.url)) {
                    return;
                }
                // If the clicked item is not this item, collapse...
                if (this.item !== clickedItem) {
                    this.collapse();
                }
            }
        });
        // Check if the url can be found in
        // one of the children of this item
        if (this.isUrlInChildren(this.item, this._router.url)) {
            this.expand();
        }
        else {
            this.collapse();
        }
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
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Toggle collapse
     *
     * @param ev
     */
    toggleOpen(ev) {
        ev.preventDefault();
        this.isOpen = !this.isOpen;
        // Navigation collapse toggled...
        this._fuseNavigationService.onItemCollapsed.next(this.item);
        this._fuseNavigationService.onItemCollapseToggled.next();
    }
    /**
     * Expand the collapsable navigation
     */
    expand() {
        if (this.isOpen) {
            return;
        }
        this.isOpen = true;
        // Mark for check
        this._changeDetectorRef.markForCheck();
        this._fuseNavigationService.onItemCollapseToggled.next();
    }
    /**
     * Collapse the collapsable navigation
     */
    collapse() {
        if (!this.isOpen) {
            return;
        }
        this.isOpen = false;
        // Mark for check
        this._changeDetectorRef.markForCheck();
        this._fuseNavigationService.onItemCollapseToggled.next();
    }
    /**
     * Check if the given parent has the
     * given item in one of its children
     *
     * @param parent
     * @param item
     * @returns {boolean}
     */
    isChildrenOf(parent, item) {
        if (!parent.children) {
            return false;
        }
        if (parent.children.indexOf(item) !== -1) {
            return true;
        }
        for (const children of parent.children) {
            if (children.children) {
                return this.isChildrenOf(children, item);
            }
        }
    }
    /**
     * Check if the given url can be found
     * in one of the given parent's children
     *
     * @param parent
     * @param url
     * @returns {boolean}
     */
    isUrlInChildren(parent, url) {
        if (!parent.children) {
            return false;
        }
        for (let i = 0; i < parent.children.length; i++) {
            if (parent.children[i].children) {
                if (this.isUrlInChildren(parent.children[i], url)) {
                    return true;
                }
            }
            if (parent.children[i].url === url || url.includes(parent.children[i].url)) {
                return true;
            }
        }
        return false;
    }
};
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", Object)
], FuseNavVerticalCollapsableComponent.prototype, "item", void 0);
tslib_1.__decorate([
    HostBinding('class'),
    tslib_1.__metadata("design:type", Object)
], FuseNavVerticalCollapsableComponent.prototype, "classes", void 0);
tslib_1.__decorate([
    HostBinding('class.open'),
    tslib_1.__metadata("design:type", Object)
], FuseNavVerticalCollapsableComponent.prototype, "isOpen", void 0);
FuseNavVerticalCollapsableComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-nav-vertical-collapsable',
        templateUrl: './collapsable.component.html',
        styleUrls: ['./collapsable.component.scss'],
        animations: fuseAnimations
    }),
    tslib_1.__metadata("design:paramtypes", [ChangeDetectorRef,
        FuseNavigationService,
        Router])
], FuseNavVerticalCollapsableComponent);
export { FuseNavVerticalCollapsableComponent };
//# sourceMappingURL=collapsable.component.js.map