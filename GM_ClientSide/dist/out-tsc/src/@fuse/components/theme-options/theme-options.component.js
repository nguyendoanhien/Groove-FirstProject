import * as tslib_1 from "tslib";
import { Component, HostBinding, Inject, Renderer2, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { DOCUMENT } from '@angular/common';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfigService } from '@fuse/services/config.service';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
let FuseThemeOptionsComponent = class FuseThemeOptionsComponent {
    /**
     * Constructor
     *
     * @param {DOCUMENT} document
     * @param {FormBuilder} _formBuilder
     * @param {FuseConfigService} _fuseConfigService
     * @param {FuseNavigationService} _fuseNavigationService
     * @param {FuseSidebarService} _fuseSidebarService
     * @param {Renderer2} _renderer
     */
    constructor(document, _formBuilder, _fuseConfigService, _fuseNavigationService, _fuseSidebarService, _renderer) {
        this.document = document;
        this._formBuilder = _formBuilder;
        this._fuseConfigService = _fuseConfigService;
        this._fuseNavigationService = _fuseNavigationService;
        this._fuseSidebarService = _fuseSidebarService;
        this._renderer = _renderer;
        // Set the defaults
        this.barClosed = true;
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
        // Build the config form
        // noinspection TypeScriptValidateTypes
        this.form = this._formBuilder.group({
            colorTheme: new FormControl(),
            customScrollbars: new FormControl(),
            layout: this._formBuilder.group({
                style: new FormControl(),
                width: new FormControl(),
                navbar: this._formBuilder.group({
                    primaryBackground: new FormControl(),
                    secondaryBackground: new FormControl(),
                    folded: new FormControl(),
                    hidden: new FormControl(),
                    position: new FormControl(),
                    variant: new FormControl()
                }),
                toolbar: this._formBuilder.group({
                    background: new FormControl(),
                    customBackgroundColor: new FormControl(),
                    hidden: new FormControl(),
                    position: new FormControl()
                }),
                footer: this._formBuilder.group({
                    background: new FormControl(),
                    customBackgroundColor: new FormControl(),
                    hidden: new FormControl(),
                    position: new FormControl()
                }),
                sidepanel: this._formBuilder.group({
                    hidden: new FormControl(),
                    position: new FormControl()
                })
            })
        });
        // Subscribe to the config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
            // Update the stored config
            this.fuseConfig = config;
            // Set the config form values without emitting an event
            // so that we don't end up with an infinite loop
            this.form.setValue(config, { emitEvent: false });
        });
        // Subscribe to the specific form value changes (layout.style)
        this.form.get('layout.style').valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((value) => {
            // Reset the form values based on the
            // selected layout style
            this._resetFormValues(value);
        });
        // Subscribe to the form value changes
        this.form.valueChanges
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
            // Update the config
            this._fuseConfigService.config = config;
        });
        // Add customize nav item that opens the bar programmatically
        const customFunctionNavItem = {
            id: 'custom-function',
            title: 'Custom Function',
            type: 'group',
            icon: 'settings',
            children: [
                {
                    id: 'customize',
                    title: 'Customize',
                    type: 'item',
                    icon: 'settings',
                    function: () => {
                        this.toggleSidebarOpen('themeOptionsPanel');
                    }
                }
            ]
        };
        this._fuseNavigationService.addNavigationItem(customFunctionNavItem, 'end');
    }
    /**
     * On destroy
     */
    ngOnDestroy() {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
        // Remove the custom function menu
        this._fuseNavigationService.removeNavigationItem('custom-function');
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Reset the form values based on the
     * selected layout style
     *
     * @param value
     * @private
     */
    _resetFormValues(value) {
        switch (value) {
            // Vertical Layout #1
            case 'vertical-layout-1':
                {
                    this.form.patchValue({
                        layout: {
                            width: 'fullwidth',
                            navbar: {
                                primaryBackground: 'fuse-navy-700',
                                secondaryBackground: 'fuse-navy-900',
                                folded: false,
                                hidden: false,
                                position: 'left',
                                variant: 'vertical-style-1'
                            },
                            toolbar: {
                                background: 'fuse-white-500',
                                customBackgroundColor: false,
                                hidden: false,
                                position: 'below-static'
                            },
                            footer: {
                                background: 'fuse-navy-900',
                                customBackgroundColor: true,
                                hidden: false,
                                position: 'below-static'
                            },
                            sidepanel: {
                                hidden: false,
                                position: 'right'
                            }
                        }
                    });
                    break;
                }
            // Vertical Layout #2
            case 'vertical-layout-2':
                {
                    this.form.patchValue({
                        layout: {
                            width: 'fullwidth',
                            navbar: {
                                primaryBackground: 'fuse-navy-700',
                                secondaryBackground: 'fuse-navy-900',
                                folded: false,
                                hidden: false,
                                position: 'left',
                                variant: 'vertical-style-1'
                            },
                            toolbar: {
                                background: 'fuse-white-500',
                                customBackgroundColor: false,
                                hidden: false,
                                position: 'below'
                            },
                            footer: {
                                background: 'fuse-navy-900',
                                customBackgroundColor: true,
                                hidden: false,
                                position: 'below'
                            },
                            sidepanel: {
                                hidden: false,
                                position: 'right'
                            }
                        }
                    });
                    break;
                }
            // Vertical Layout #3
            case 'vertical-layout-3':
                {
                    this.form.patchValue({
                        layout: {
                            width: 'fullwidth',
                            navbar: {
                                primaryBackground: 'fuse-navy-700',
                                secondaryBackground: 'fuse-navy-900',
                                folded: false,
                                hidden: false,
                                position: 'left',
                                layout: 'vertical-style-1'
                            },
                            toolbar: {
                                background: 'fuse-white-500',
                                customBackgroundColor: false,
                                hidden: false,
                                position: 'above-static'
                            },
                            footer: {
                                background: 'fuse-navy-900',
                                customBackgroundColor: true,
                                hidden: false,
                                position: 'above-static'
                            },
                            sidepanel: {
                                hidden: false,
                                position: 'right'
                            }
                        }
                    });
                    break;
                }
            // Horizontal Layout #1
            case 'horizontal-layout-1':
                {
                    this.form.patchValue({
                        layout: {
                            width: 'fullwidth',
                            navbar: {
                                primaryBackground: 'fuse-navy-700',
                                secondaryBackground: 'fuse-navy-900',
                                folded: false,
                                hidden: false,
                                position: 'top',
                                variant: 'vertical-style-1'
                            },
                            toolbar: {
                                background: 'fuse-white-500',
                                customBackgroundColor: false,
                                hidden: false,
                                position: 'above'
                            },
                            footer: {
                                background: 'fuse-navy-900',
                                customBackgroundColor: true,
                                hidden: false,
                                position: 'above-fixed'
                            },
                            sidepanel: {
                                hidden: false,
                                position: 'right'
                            }
                        }
                    });
                    break;
                }
        }
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
};
tslib_1.__decorate([
    HostBinding('class.bar-closed'),
    tslib_1.__metadata("design:type", Boolean)
], FuseThemeOptionsComponent.prototype, "barClosed", void 0);
FuseThemeOptionsComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-theme-options',
        templateUrl: './theme-options.component.html',
        styleUrls: ['./theme-options.component.scss'],
        encapsulation: ViewEncapsulation.None,
        animations: fuseAnimations
    }),
    tslib_1.__param(0, Inject(DOCUMENT)),
    tslib_1.__metadata("design:paramtypes", [Object, FormBuilder,
        FuseConfigService,
        FuseNavigationService,
        FuseSidebarService,
        Renderer2])
], FuseThemeOptionsComponent);
export { FuseThemeOptionsComponent };
//# sourceMappingURL=theme-options.component.js.map