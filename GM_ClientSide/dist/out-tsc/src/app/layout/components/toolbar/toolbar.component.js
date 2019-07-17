import * as tslib_1 from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';
import * as _ from 'lodash';
import { FuseConfigService } from '@fuse/services/config.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { navigation } from 'app/navigation/navigation';
let ToolbarComponent = class ToolbarComponent {
    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FuseSidebarService} _fuseSidebarService
     * @param {TranslateService} _translateService
     */
    constructor(_fuseConfigService, _fuseSidebarService, _translateService) {
        this._fuseConfigService = _fuseConfigService;
        this._fuseSidebarService = _fuseSidebarService;
        this._translateService = _translateService;
        // Set the defaults
        this.userStatusOptions = [
            {
                title: 'Online',
                icon: 'icon-checkbox-marked-circle',
                color: '#4CAF50'
            },
            {
                title: 'Away',
                icon: 'icon-clock',
                color: '#FFC107'
            },
            {
                title: 'Do not Disturb',
                icon: 'icon-minus-circle',
                color: '#F44336'
            },
            {
                title: 'Invisible',
                icon: 'icon-checkbox-blank-circle-outline',
                color: '#BDBDBD'
            },
            {
                title: 'Offline',
                icon: 'icon-checkbox-blank-circle-outline',
                color: '#616161'
            }
        ];
        this.languages = [
            {
                id: 'en',
                title: 'English',
                flag: 'us'
            },
            {
                id: 'tr',
                title: 'Turkish',
                flag: 'tr'
            }
        ];
        this.navigation = navigation;
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
        // Subscribe to the config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((settings) => {
            this.horizontalNavbar = settings.layout.navbar.position === 'top';
            this.rightNavbar = settings.layout.navbar.position === 'right';
            this.hiddenNavbar = settings.layout.navbar.hidden === true;
        });
        // Set the selected language from default languages
        this.selectedLanguage = _.find(this.languages, { id: this._translateService.currentLang });
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
     * Toggle sidebar open
     *
     * @param key
     */
    toggleSidebarOpen(key) {
        this._fuseSidebarService.getSidebar(key).toggleOpen();
    }
    /**
     * Search
     *
     * @param value
     */
    search(value) {
        // Do your search here...
        console.log(value);
    }
    /**
     * Set the language
     *
     * @param lang
     */
    setLanguage(lang) {
        // Set the selected language for the toolbar
        this.selectedLanguage = lang;
        // Use the selected language for translations
        this._translateService.use(lang.id);
    }
};
ToolbarComponent = tslib_1.__decorate([
    Component({
        selector: 'toolbar',
        templateUrl: './toolbar.component.html',
        styleUrls: ['./toolbar.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [FuseConfigService,
        FuseSidebarService,
        TranslateService])
], ToolbarComponent);
export { ToolbarComponent };
//# sourceMappingURL=toolbar.component.js.map