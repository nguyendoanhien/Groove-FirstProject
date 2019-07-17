import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { VerticalLayout1Module } from 'app/layout/vertical/layout-1/layout-1.module';
import { VerticalLayout2Module } from 'app/layout/vertical/layout-2/layout-2.module';
import { VerticalLayout3Module } from 'app/layout/vertical/layout-3/layout-3.module';
import { HorizontalLayout1Module } from 'app/layout/horizontal/layout-1/layout-1.module';
let LayoutModule = class LayoutModule {
};
LayoutModule = tslib_1.__decorate([
    NgModule({
        imports: [
            VerticalLayout1Module,
            VerticalLayout2Module,
            VerticalLayout3Module,
            HorizontalLayout1Module
        ],
        exports: [
            VerticalLayout1Module,
            VerticalLayout2Module,
            VerticalLayout3Module,
            HorizontalLayout1Module
        ]
    })
], LayoutModule);
export { LayoutModule };
//# sourceMappingURL=layout.module.js.map