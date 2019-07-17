import * as tslib_1 from "tslib";
import { Directive, ElementRef } from '@angular/core';
let FuseWidgetToggleDirective = class FuseWidgetToggleDirective {
    /**
     * Constructor
     *
     * @param {ElementRef} elementRef
     */
    constructor(elementRef) {
        this.elementRef = elementRef;
    }
};
FuseWidgetToggleDirective = tslib_1.__decorate([
    Directive({
        selector: '[fuseWidgetToggle]'
    }),
    tslib_1.__metadata("design:paramtypes", [ElementRef])
], FuseWidgetToggleDirective);
export { FuseWidgetToggleDirective };
//# sourceMappingURL=widget-toggle.directive.js.map