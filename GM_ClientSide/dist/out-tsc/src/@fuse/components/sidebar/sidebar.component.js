import * as tslib_1 from "tslib";
import { ChangeDetectorRef, Component, ElementRef, EventEmitter, HostBinding, HostListener, Input, Output, Renderer2, ViewEncapsulation } from '@angular/core';
import { animate, AnimationBuilder, style } from '@angular/animations';
import { MediaObserver } from '@angular/flex-layout';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseSidebarService } from './sidebar.service';
import { FuseMatchMediaService } from '@fuse/services/match-media.service';
import { FuseConfigService } from '@fuse/services/config.service';
let FuseSidebarComponent = class FuseSidebarComponent {
    /**
     * Constructor
     *
     * @param {AnimationBuilder} _animationBuilder
     * @param {ChangeDetectorRef} _changeDetectorRef
     * @param {ElementRef} _elementRef
     * @param {FuseConfigService} _fuseConfigService
     * @param {FuseMatchMediaService} _fuseMatchMediaService
     * @param {FuseSidebarService} _fuseSidebarService
     * @param {MediaObserver} _mediaObserver
     * @param {Renderer2} _renderer
     */
    constructor(_animationBuilder, _changeDetectorRef, _elementRef, _fuseConfigService, _fuseMatchMediaService, _fuseSidebarService, _mediaObserver, _renderer) {
        this._animationBuilder = _animationBuilder;
        this._changeDetectorRef = _changeDetectorRef;
        this._elementRef = _elementRef;
        this._fuseConfigService = _fuseConfigService;
        this._fuseMatchMediaService = _fuseMatchMediaService;
        this._fuseSidebarService = _fuseSidebarService;
        this._mediaObserver = _mediaObserver;
        this._renderer = _renderer;
        this._backdrop = null;
        // Set the defaults
        this.foldedAutoTriggerOnHover = true;
        this.foldedWidth = 64;
        this.foldedChanged = new EventEmitter();
        this.openedChanged = new EventEmitter();
        this.opened = false;
        this.position = 'left';
        this.invisibleOverlay = false;
        // Set the private defaults
        this._animationsEnabled = false;
        this._folded = false;
        this._unsubscribeAll = new Subject();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------
    /**
     * Folded
     *
     * @param {boolean} value
     */
    set folded(value) {
        // Set the folded
        this._folded = value;
        // Return if the sidebar is closed
        if (!this.opened) {
            return;
        }
        // Programmatically add/remove padding to the element
        // that comes after or before based on the position
        let sibling, styleRule;
        const styleValue = this.foldedWidth + 'px';
        // Get the sibling and set the style rule
        if (this.position === 'left') {
            sibling = this._elementRef.nativeElement.nextElementSibling;
            styleRule = 'padding-left';
        }
        else {
            sibling = this._elementRef.nativeElement.previousElementSibling;
            styleRule = 'padding-right';
        }
        // If there is no sibling, return...
        if (!sibling) {
            return;
        }
        // If folded...
        if (value) {
            // Fold the sidebar
            this.fold();
            // Set the folded width
            this._renderer.setStyle(this._elementRef.nativeElement, 'width', styleValue);
            this._renderer.setStyle(this._elementRef.nativeElement, 'min-width', styleValue);
            this._renderer.setStyle(this._elementRef.nativeElement, 'max-width', styleValue);
            // Set the style and class
            this._renderer.setStyle(sibling, styleRule, styleValue);
            this._renderer.addClass(this._elementRef.nativeElement, 'folded');
        }
        // If unfolded...
        else {
            // Unfold the sidebar
            this.unfold();
            // Remove the folded width
            this._renderer.removeStyle(this._elementRef.nativeElement, 'width');
            this._renderer.removeStyle(this._elementRef.nativeElement, 'min-width');
            this._renderer.removeStyle(this._elementRef.nativeElement, 'max-width');
            // Remove the style and class
            this._renderer.removeStyle(sibling, styleRule);
            this._renderer.removeClass(this._elementRef.nativeElement, 'folded');
        }
        // Emit the 'foldedChanged' event
        this.foldedChanged.emit(this.folded);
    }
    get folded() {
        return this._folded;
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------
    /**
     * On init
     */
    ngOnInit() {
        // Subscribe to config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((config) => {
            this._fuseConfig = config;
        });
        // Register the sidebar
        this._fuseSidebarService.register(this.name, this);
        // Setup visibility
        this._setupVisibility();
        // Setup position
        this._setupPosition();
        // Setup lockedOpen
        this._setupLockedOpen();
        // Setup folded
        this._setupFolded();
    }
    /**
     * On destroy
     */
    ngOnDestroy() {
        // If the sidebar is folded, unfold it to revert modifications
        if (this.folded) {
            this.unfold();
        }
        // Unregister the sidebar
        this._fuseSidebarService.unregister(this.name);
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Setup the visibility of the sidebar
     *
     * @private
     */
    _setupVisibility() {
        // Remove the existing box-shadow
        this._renderer.setStyle(this._elementRef.nativeElement, 'box-shadow', 'none');
        // Make the sidebar invisible
        this._renderer.setStyle(this._elementRef.nativeElement, 'visibility', 'hidden');
    }
    /**
     * Setup the sidebar position
     *
     * @private
     */
    _setupPosition() {
        // Add the correct class name to the sidebar
        // element depending on the position attribute
        if (this.position === 'right') {
            this._renderer.addClass(this._elementRef.nativeElement, 'right-positioned');
        }
        else {
            this._renderer.addClass(this._elementRef.nativeElement, 'left-positioned');
        }
    }
    /**
     * Setup the lockedOpen handler
     *
     * @private
     */
    _setupLockedOpen() {
        // Return if the lockedOpen wasn't set
        if (!this.lockedOpen) {
            // Return
            return;
        }
        // Set the wasActive for the first time
        this._wasActive = false;
        // Set the wasFolded
        this._wasFolded = this.folded;
        // Show the sidebar
        this._showSidebar();
        // Act on every media change
        this._fuseMatchMediaService.onMediaChange
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(() => {
            // Get the active status
            const isActive = this._mediaObserver.isActive(this.lockedOpen);
            // If the both status are the same, don't act
            if (this._wasActive === isActive) {
                return;
            }
            // Activate the lockedOpen
            if (isActive) {
                // Set the lockedOpen status
                this.isLockedOpen = true;
                // Show the sidebar
                this._showSidebar();
                // Force the the opened status to true
                this.opened = true;
                // Emit the 'openedChanged' event
                this.openedChanged.emit(this.opened);
                // If the sidebar was folded, forcefully fold it again
                if (this._wasFolded) {
                    // Enable the animations
                    this._enableAnimations();
                    // Fold
                    this.folded = true;
                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                }
                // Hide the backdrop if any exists
                this._hideBackdrop();
            }
            // De-Activate the lockedOpen
            else {
                // Set the lockedOpen status
                this.isLockedOpen = false;
                // Unfold the sidebar in case if it was folded
                this.unfold();
                // Force the the opened status to close
                this.opened = false;
                // Emit the 'openedChanged' event
                this.openedChanged.emit(this.opened);
                // Hide the sidebar
                this._hideSidebar();
            }
            // Store the new active status
            this._wasActive = isActive;
        });
    }
    /**
     * Setup the initial folded status
     *
     * @private
     */
    _setupFolded() {
        // Return, if sidebar is not folded
        if (!this.folded) {
            return;
        }
        // Return if the sidebar is closed
        if (!this.opened) {
            return;
        }
        // Programmatically add/remove padding to the element
        // that comes after or before based on the position
        let sibling, styleRule;
        const styleValue = this.foldedWidth + 'px';
        // Get the sibling and set the style rule
        if (this.position === 'left') {
            sibling = this._elementRef.nativeElement.nextElementSibling;
            styleRule = 'padding-left';
        }
        else {
            sibling = this._elementRef.nativeElement.previousElementSibling;
            styleRule = 'padding-right';
        }
        // If there is no sibling, return...
        if (!sibling) {
            return;
        }
        // Fold the sidebar
        this.fold();
        // Set the folded width
        this._renderer.setStyle(this._elementRef.nativeElement, 'width', styleValue);
        this._renderer.setStyle(this._elementRef.nativeElement, 'min-width', styleValue);
        this._renderer.setStyle(this._elementRef.nativeElement, 'max-width', styleValue);
        // Set the style and class
        this._renderer.setStyle(sibling, styleRule, styleValue);
        this._renderer.addClass(this._elementRef.nativeElement, 'folded');
    }
    /**
     * Show the backdrop
     *
     * @private
     */
    _showBackdrop() {
        // Create the backdrop element
        this._backdrop = this._renderer.createElement('div');
        // Add a class to the backdrop element
        this._backdrop.classList.add('fuse-sidebar-overlay');
        // Add a class depending on the invisibleOverlay option
        if (this.invisibleOverlay) {
            this._backdrop.classList.add('fuse-sidebar-overlay-invisible');
        }
        // Append the backdrop to the parent of the sidebar
        this._renderer.appendChild(this._elementRef.nativeElement.parentElement, this._backdrop);
        // Create the enter animation and attach it to the player
        this._player =
            this._animationBuilder
                .build([
                animate('300ms ease', style({ opacity: 1 }))
            ]).create(this._backdrop);
        // Play the animation
        this._player.play();
        // Add an event listener to the overlay
        this._backdrop.addEventListener('click', () => {
            this.close();
        });
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Hide the backdrop
     *
     * @private
     */
    _hideBackdrop() {
        if (!this._backdrop) {
            return;
        }
        // Create the leave animation and attach it to the player
        this._player =
            this._animationBuilder
                .build([
                animate('300ms ease', style({ opacity: 0 }))
            ]).create(this._backdrop);
        // Play the animation
        this._player.play();
        // Once the animation is done...
        this._player.onDone(() => {
            // If the backdrop still exists...
            if (this._backdrop) {
                // Remove the backdrop
                this._backdrop.parentNode.removeChild(this._backdrop);
                this._backdrop = null;
            }
        });
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Change some properties of the sidebar
     * and make it visible
     *
     * @private
     */
    _showSidebar() {
        // Remove the box-shadow style
        this._renderer.removeStyle(this._elementRef.nativeElement, 'box-shadow');
        // Make the sidebar invisible
        this._renderer.removeStyle(this._elementRef.nativeElement, 'visibility');
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Change some properties of the sidebar
     * and make it invisible
     *
     * @private
     */
    _hideSidebar(delay = true) {
        const delayAmount = delay ? 300 : 0;
        // Add a delay so close animation can play
        setTimeout(() => {
            // Remove the box-shadow
            this._renderer.setStyle(this._elementRef.nativeElement, 'box-shadow', 'none');
            // Make the sidebar invisible
            this._renderer.setStyle(this._elementRef.nativeElement, 'visibility', 'hidden');
        }, delayAmount);
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Enable the animations
     *
     * @private
     */
    _enableAnimations() {
        // Return if animations already enabled
        if (this._animationsEnabled) {
            return;
        }
        // Enable the animations
        this._animationsEnabled = true;
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Open the sidebar
     */
    open() {
        if (this.opened || this.isLockedOpen) {
            return;
        }
        // Enable the animations
        this._enableAnimations();
        // Show the sidebar
        this._showSidebar();
        // Show the backdrop
        this._showBackdrop();
        // Set the opened status
        this.opened = true;
        // Emit the 'openedChanged' event
        this.openedChanged.emit(this.opened);
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Close the sidebar
     */
    close() {
        if (!this.opened || this.isLockedOpen) {
            return;
        }
        // Enable the animations
        this._enableAnimations();
        // Hide the backdrop
        this._hideBackdrop();
        // Set the opened status
        this.opened = false;
        // Emit the 'openedChanged' event
        this.openedChanged.emit(this.opened);
        // Hide the sidebar
        this._hideSidebar();
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Toggle open/close the sidebar
     */
    toggleOpen() {
        if (this.opened) {
            this.close();
        }
        else {
            this.open();
        }
    }
    /**
     * Mouseenter
     */
    onMouseEnter() {
        // Only work if the auto trigger is enabled
        if (!this.foldedAutoTriggerOnHover) {
            return;
        }
        this.unfoldTemporarily();
    }
    /**
     * Mouseleave
     */
    onMouseLeave() {
        // Only work if the auto trigger is enabled
        if (!this.foldedAutoTriggerOnHover) {
            return;
        }
        this.foldTemporarily();
    }
    /**
     * Fold the sidebar permanently
     */
    fold() {
        // Only work if the sidebar is not folded
        if (this.folded) {
            return;
        }
        // Enable the animations
        this._enableAnimations();
        // Fold
        this.folded = true;
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Unfold the sidebar permanently
     */
    unfold() {
        // Only work if the sidebar is folded
        if (!this.folded) {
            return;
        }
        // Enable the animations
        this._enableAnimations();
        // Unfold
        this.folded = false;
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Toggle the sidebar fold/unfold permanently
     */
    toggleFold() {
        if (this.folded) {
            this.unfold();
        }
        else {
            this.fold();
        }
    }
    /**
     * Fold the temporarily unfolded sidebar back
     */
    foldTemporarily() {
        // Only work if the sidebar is folded
        if (!this.folded) {
            return;
        }
        // Enable the animations
        this._enableAnimations();
        // Fold the sidebar back
        this.unfolded = false;
        // Set the folded width
        const styleValue = this.foldedWidth + 'px';
        this._renderer.setStyle(this._elementRef.nativeElement, 'width', styleValue);
        this._renderer.setStyle(this._elementRef.nativeElement, 'min-width', styleValue);
        this._renderer.setStyle(this._elementRef.nativeElement, 'max-width', styleValue);
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
    /**
     * Unfold the sidebar temporarily
     */
    unfoldTemporarily() {
        // Only work if the sidebar is folded
        if (!this.folded) {
            return;
        }
        // Enable the animations
        this._enableAnimations();
        // Unfold the sidebar temporarily
        this.unfolded = true;
        // Remove the folded width
        this._renderer.removeStyle(this._elementRef.nativeElement, 'width');
        this._renderer.removeStyle(this._elementRef.nativeElement, 'min-width');
        this._renderer.removeStyle(this._elementRef.nativeElement, 'max-width');
        // Mark for check
        this._changeDetectorRef.markForCheck();
    }
};
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String)
], FuseSidebarComponent.prototype, "name", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String)
], FuseSidebarComponent.prototype, "key", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String)
], FuseSidebarComponent.prototype, "position", void 0);
tslib_1.__decorate([
    HostBinding('class.open'),
    tslib_1.__metadata("design:type", Boolean)
], FuseSidebarComponent.prototype, "opened", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", String)
], FuseSidebarComponent.prototype, "lockedOpen", void 0);
tslib_1.__decorate([
    HostBinding('class.locked-open'),
    tslib_1.__metadata("design:type", Boolean)
], FuseSidebarComponent.prototype, "isLockedOpen", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", Number)
], FuseSidebarComponent.prototype, "foldedWidth", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", Boolean)
], FuseSidebarComponent.prototype, "foldedAutoTriggerOnHover", void 0);
tslib_1.__decorate([
    HostBinding('class.unfolded'),
    tslib_1.__metadata("design:type", Boolean)
], FuseSidebarComponent.prototype, "unfolded", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", Boolean)
], FuseSidebarComponent.prototype, "invisibleOverlay", void 0);
tslib_1.__decorate([
    Output(),
    tslib_1.__metadata("design:type", EventEmitter)
], FuseSidebarComponent.prototype, "foldedChanged", void 0);
tslib_1.__decorate([
    Output(),
    tslib_1.__metadata("design:type", EventEmitter)
], FuseSidebarComponent.prototype, "openedChanged", void 0);
tslib_1.__decorate([
    HostBinding('class.animations-enabled'),
    tslib_1.__metadata("design:type", Boolean)
], FuseSidebarComponent.prototype, "_animationsEnabled", void 0);
tslib_1.__decorate([
    Input(),
    tslib_1.__metadata("design:type", Boolean),
    tslib_1.__metadata("design:paramtypes", [Boolean])
], FuseSidebarComponent.prototype, "folded", null);
tslib_1.__decorate([
    HostListener('mouseenter'),
    tslib_1.__metadata("design:type", Function),
    tslib_1.__metadata("design:paramtypes", []),
    tslib_1.__metadata("design:returntype", void 0)
], FuseSidebarComponent.prototype, "onMouseEnter", null);
tslib_1.__decorate([
    HostListener('mouseleave'),
    tslib_1.__metadata("design:type", Function),
    tslib_1.__metadata("design:paramtypes", []),
    tslib_1.__metadata("design:returntype", void 0)
], FuseSidebarComponent.prototype, "onMouseLeave", null);
FuseSidebarComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-sidebar',
        templateUrl: './sidebar.component.html',
        styleUrls: ['./sidebar.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [AnimationBuilder,
        ChangeDetectorRef,
        ElementRef,
        FuseConfigService,
        FuseMatchMediaService,
        FuseSidebarService,
        MediaObserver,
        Renderer2])
], FuseSidebarComponent);
export { FuseSidebarComponent };
//# sourceMappingURL=sidebar.component.js.map