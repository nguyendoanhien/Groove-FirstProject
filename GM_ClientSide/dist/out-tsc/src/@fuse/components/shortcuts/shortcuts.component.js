import * as tslib_1 from "tslib";
import { Component, ElementRef, Input, Renderer2, ViewChild } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { CookieService } from 'ngx-cookie-service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseMatchMediaService } from '@fuse/services/match-media.service';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
let FuseShortcutsComponent = class FuseShortcutsComponent {
    /**
     * Constructor
     *
     * @param {CookieService} _cookieService
     * @param {FuseMatchMediaService} _fuseMatchMediaService
     * @param {FuseNavigationService} _fuseNavigationService
     * @param {MediaObserver} _mediaObserver
     * @param {Renderer2} _renderer
     */
    constructor(_cookieService, _fuseMatchMediaService, _fuseNavigationService, _mediaObserver, _renderer) {
        this._cookieService = _cookieService;
        this._fuseMatchMediaService = _fuseMatchMediaService;
        this._fuseNavigationService = _fuseNavigationService;
        this._mediaObserver = _mediaObserver;
        this._renderer = _renderer;
        // Set the defaults
        this.shortcutItems = [];
        this.searching = false;
        this.mobileShortcutsPanelActive = false;
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
        // Get the navigation items and flatten them
        this.filteredNavigationItems = this.navigationItems = this._fuseNavigationService.getFlatNavigation(this.navigation);
        if (this._cookieService.check('FUSE2.shortcuts')) {
            this.shortcutItems = JSON.parse(this._cookieService.get('FUSE2.shortcuts'));
        }
        else {
            // User's shortcut items
            this.shortcutItems = [
                {
                    title: 'Calendar',
                    type: 'item',
                    icon: 'today',
                    url: '/apps/calendar'
                },
                {
                    title: 'Mail',
                    type: 'item',
                    icon: 'email',
                    url: '/apps/mail'
                },
                {
                    title: 'Contacts',
                    type: 'item',
                    icon: 'account_box',
                    url: '/apps/contacts'
                },
                {
                    title: 'To-Do',
                    type: 'item',
                    icon: 'check_box',
                    url: '/apps/todo'
                }
            ];
        }
    }
    ngAfterViewInit() {
        // Subscribe to media changes
        this._fuseMatchMediaService.onMediaChange
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
            if (this._mediaObserver.isActive('gt-sm')) {
                this.hideMobileShortcutsPanel();
            }
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
     * Search
     *
     * @param event
     */
    search(event) {
        const value = event.target.value.toLowerCase();
        if (value === '') {
            this.searching = false;
            this.filteredNavigationItems = this.navigationItems;
            return;
        }
        this.searching = true;
        this.filteredNavigationItems = this.navigationItems.filter((navigationItem) => {
            return navigationItem.title.toLowerCase().includes(value);
        });
    }
    /**
     * Toggle shortcut
     *
     * @param event
     * @param itemToToggle
     */
    toggleShortcut(event, itemToToggle) {
        event.stopPropagation();
        for (let i = 0; i < this.shortcutItems.length; i++) {
            if (this.shortcutItems[i].url === itemToToggle.url) {
                this.shortcutItems.splice(i, 1);
                // Save to the cookies
                this._cookieService.set('FUSE2.shortcuts', JSON.stringify(this.shortcutItems));
                return;
            }
        }
        this.shortcutItems.push(itemToToggle);
        // Save to the cookies
        this._cookieService.set('FUSE2.shortcuts', JSON.stringify(this.shortcutItems));
    }
    /**
     * Is in shortcuts?
     *
     * @param navigationItem
     * @returns {any}
     */
    isInShortcuts(navigationItem) {
        return this.shortcutItems.find(item => {
            return item.url === navigationItem.url;
        });
    }
    /**
     * On menu open
     */
    onMenuOpen() {
        setTimeout(() => {
            this.searchInputField.nativeElement.focus();
        });
    }
    /**
     * Show mobile shortcuts
     */
    showMobileShortcutsPanel() {
        this.mobileShortcutsPanelActive = true;
        this._renderer.addClass(this.shortcutsEl.nativeElement, 'show-mobile-panel');
    }
    /**
     * Hide mobile shortcuts
     */
    hideMobileShortcutsPanel() {
        this.mobileShortcutsPanelActive = false;
        this._renderer.removeClass(this.shortcutsEl.nativeElement, 'show-mobile-panel');
    }
};
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", Object)
], FuseShortcutsComponent.prototype, "navigation", void 0);
tslib_1.__decorate([
    ViewChild('searchInput', { static: false }),
    tslib_1.__metadata("design:type", Object)
], FuseShortcutsComponent.prototype, "searchInputField", void 0);
tslib_1.__decorate([
    ViewChild('shortcuts', { static: false }),
    tslib_1.__metadata("design:type", ElementRef)
], FuseShortcutsComponent.prototype, "shortcutsEl", void 0);
FuseShortcutsComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-shortcuts',
        templateUrl: './shortcuts.component.html',
        styleUrls: ['./shortcuts.component.scss']
    }),
    tslib_1.__metadata("design:paramtypes", [CookieService,
        FuseMatchMediaService,
        FuseNavigationService,
        MediaObserver,
        Renderer2])
], FuseShortcutsComponent);
export { FuseShortcutsComponent };
//# sourceMappingURL=shortcuts.component.js.map