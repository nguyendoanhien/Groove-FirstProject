import * as tslib_1 from "tslib";
import { Component, ContentChildren, ElementRef, HostBinding, QueryList, Renderer2, ViewEncapsulation } from '@angular/core';
import { FuseWidgetToggleDirective } from './widget-toggle.directive';
let FuseWidgetComponent = class FuseWidgetComponent {
    /**
     * Constructor
     *
     * @param {ElementRef} _elementRef
     * @param {Renderer2} _renderer
     */
    constructor(_elementRef, _renderer) {
        this._elementRef = _elementRef;
        this._renderer = _renderer;
        this.flipped = false;
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------
    /**
     * After content init
     */
    ngAfterContentInit() {
        // Listen for the flip button click
        setTimeout(() => {
            this.toggleButtons.forEach(flipButton => {
                this._renderer.listen(flipButton.elementRef.nativeElement, 'click', (event) => {
                    event.preventDefault();
                    event.stopPropagation();
                    this.toggle();
                });
            });
        });
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------
    /**
     * Toggle the flipped status
     */
    toggle() {
        this.flipped = !this.flipped;
    }
};
tslib_1.__decorate([
    HostBinding('class.flipped'),
    tslib_1.__metadata("design:type", Object)
], FuseWidgetComponent.prototype, "flipped", void 0);
tslib_1.__decorate([
    ContentChildren(FuseWidgetToggleDirective, { descendants: true }),
    tslib_1.__metadata("design:type", QueryList)
], FuseWidgetComponent.prototype, "toggleButtons", void 0);
FuseWidgetComponent = tslib_1.__decorate([
    Component({
        selector: 'fuse-widget',
        templateUrl: './widget.component.html',
        styleUrls: ['./widget.component.scss'],
        encapsulation: ViewEncapsulation.None
    }),
    tslib_1.__metadata("design:paramtypes", [ElementRef,
        Renderer2])
], FuseWidgetComponent);
export { FuseWidgetComponent };
//# sourceMappingURL=widget.component.js.map