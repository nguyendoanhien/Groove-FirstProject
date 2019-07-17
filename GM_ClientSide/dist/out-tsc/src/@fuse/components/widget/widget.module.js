import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { FuseWidgetComponent } from './widget.component';
import { FuseWidgetToggleDirective } from './widget-toggle.directive';
let FuseWidgetModule = class FuseWidgetModule {
};
FuseWidgetModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            FuseWidgetComponent,
            FuseWidgetToggleDirective
        ],
        exports: [
            FuseWidgetComponent,
            FuseWidgetToggleDirective
        ],
    })
], FuseWidgetModule);
export { FuseWidgetModule };
//# sourceMappingURL=widget.module.js.map