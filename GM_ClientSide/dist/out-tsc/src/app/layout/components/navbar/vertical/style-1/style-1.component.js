import * as tslib_1 from "tslib";
import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { delay, filter, take, takeUntil } from 'rxjs/operators';
import { FuseConfigService } from '@fuse/services/config.service';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { FusePerfectScrollbarDirective } from '@fuse/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
let NavbarVerticalStyle1Component = class NavbarVerticalStyle1Component {
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FuseNavigationService} _fuseNavigationService
     * @param {FuseSidebarService} _fuseSidebarService
     * @param {Router} _router
     */
    constructor(_fuseConfigService, _fuseNavigationService, _fuseSidebarService, _router) {
        this._fuseConfigService = _fuseConfigService;
        this._fuseNavigationService = _fuseNavigationService;
        this._fuseSidebarService = _fuseSidebarService;
        this._router = _router;
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------
    // Directive
    set directive(theDirective) {
        if (!theDirective) {
            return;
        }
        this._fusePerfectScrollbar = theDirective;
        // Update the scrollbar on collapsable item toggle
        this._fuseNavigationService.onItemCollapseToggled
            .pipe(delay(500), takeUntil(this._unsubscribeAll))
            .subscribe(() => {
            this._fusePerfectScrollbar.update();
        });
        // Scroll to the active item position
        this._router.events
            .pipe(filter((event) => event instanceof NavigationEnd), take(1))
            .subscribe(() => {
            setTimeout(() => {
                this._fusePerfectScrollbar.scrollToElement('navbar .nav-link.active', -120);
            });
        });
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------
    /**
     * On init
     */
    ngOnInit() {
        this._router.events
            .pipe(filter((event) => event instanceof NavigationEnd), takeUntil(this._unsubscribeAll))
            .subscribe(() => {
            if (this._fuseSidebarService.getSidebar('navbar')) {
                this._fuseSidebarService.getSidebar('navbar').close();
            }
        });
        // Subscribe to the config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
            this.fuseConfig = config;
        });
        // Get current navigation
        this._fuseNavigationService.onNavigationChanged
            .pipe(filter(value => value !== null), takeUntil(this._unsubscribeAll))
            .subscribe(() => {
            this.navigation = this._fuseNavigationService.getCurrentNavigation();
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
     * Toggle sidebar opened status
     */
    toggleSidebarOpened() {
        this._fuseSidebarService.getSidebar('navbar').toggleOpen();
    }
    /**
     * Toggle sidebar folded status
     */
    toggleSidebarFolded() {
        this._fuseSidebarService.getSidebar('navbar').toggleFold();
    }
};
tslib_1.__decorate([
    ViewChild(FusePerfectScrollbarDirective, { static: true }),
    tslib_1.__metadata("design:type", FusePerfectScrollbarDirective),
    tslib_1.__metadata("design:paramtypes", [FusePerfectScrollbarDirective])
], NavbarVerticalStyle1Component.prototype, "directive", null);
NavbarVerticalStyle1Component = tslib_1.__decorate([
    Component({
        selector: 'navbar-vertical-style-1',
        templateUrl: './style-1.component.html',
        styleUrls: ['./style-1.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [FuseConfigService,
        FuseNavigationService,
        FuseSidebarService,
        Router])
], NavbarVerticalStyle1Component);
export { NavbarVerticalStyle1Component };
//# sourceMappingURL=style-1.component.js.map