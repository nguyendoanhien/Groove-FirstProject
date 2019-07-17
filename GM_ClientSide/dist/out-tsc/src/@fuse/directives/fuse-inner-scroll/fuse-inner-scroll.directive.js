import * as tslib_1 from "tslib";
import { Directive, ElementRef, Renderer2 } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseMatchMediaService } from '@fuse/services/match-media.service';
let FuseInnerScrollDirective = class FuseInnerScrollDirective {
    /**
     * Constructor
     *
     * @param {ElementRef} _elementRef
     * @param {FuseMatchMediaService} _fuseMediaMatchService
     * @param {Renderer2} _renderer
     */
    constructor(_elementRef, _fuseMediaMatchService, _renderer) {
        this._elementRef = _elementRef;
        this._fuseMediaMatchService = _fuseMediaMatchService;
        this._renderer = _renderer;
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
        // Get the parent
        this._parent = this._renderer.parentNode(this._elementRef.nativeElement);
        // Return, if there is no parent
        if (!this._parent) {
            return;
        }
        // Get the grand parent
        this._grandParent = this._renderer.parentNode(this._parent);
        // Register to the media query changes
        this._fuseMediaMatchService.onMediaChange
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((alias) => {
            if (alias === 'xs') {
                this._removeClass();
            }
            else {
                this._addClass();
            }
        });
    }
    /**
     * On destroy
     */
    ngOnDestroy() {
        // Return, if there is no parent
        if (!this._parent) {
            return;
        }
        // Remove the class
        this._removeClass();
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Add the class name
     *
     * @private
     */
    _addClass() {
        // Add the inner-scroll class
        this._renderer.addClass(this._grandParent, 'inner-scroll');
    }
    /**
     * Remove the class name
     * @private
     */
    _removeClass() {
        // Remove the inner-scroll class
        this._renderer.removeClass(this._grandParent, 'inner-scroll');
    }
};
FuseInnerScrollDirective = tslib_1.__decorate([
    Directive({
        selector: '.inner-scroll'
    }),
    tslib_1.__metadata("design:paramtypes", [ElementRef,
        FuseMatchMediaService,
        Renderer2])
], FuseInnerScrollDirective);
export { FuseInnerScrollDirective };
//# sourceMappingURL=fuse-inner-scroll.directive.js.map