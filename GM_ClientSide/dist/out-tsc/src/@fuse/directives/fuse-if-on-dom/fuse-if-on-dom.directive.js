import * as tslib_1 from "tslib";
import { Directive, ElementRef, TemplateRef, ViewContainerRef } from '@angular/core';
let FuseIfOnDomDirective = class FuseIfOnDomDirective {
    /**
     * Constructor
     *
     * @param {ElementRef} _elementRef
     * @param {TemplateRef<any>} _templateRef
     * @param {ViewContainerRef} _viewContainerRef
     */
    constructor(_elementRef, _templateRef, _viewContainerRef) {
        this._elementRef = _elementRef;
        this._templateRef = _templateRef;
        this._viewContainerRef = _viewContainerRef;
        // Set the defaults
        this.isCreated = false;
    }
    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------
    /**
     * After content checked
     */
    ngAfterContentChecked() {
        if (document.body.contains(this._elementRef.nativeElement) && !this.isCreated) {
            setTimeout(() => {
                this._viewContainerRef.createEmbeddedView(this._templateRef);
            }, 300);
            this.isCreated = true;
        }
        else if (this.isCreated && !document.body.contains(this._elementRef.nativeElement)) {
            this._viewContainerRef.clear();
            this.isCreated = false;
        }
    }
};
FuseIfOnDomDirective = tslib_1.__decorate([
    Directive({
        selector: '[fuseIfOnDom]'
    }),
    tslib_1.__metadata("design:paramtypes", [ElementRef,
        TemplateRef,
        ViewContainerRef])
], FuseIfOnDomDirective);
export { FuseIfOnDomDirective };
//# sourceMappingURL=fuse-if-on-dom.directive.js.map