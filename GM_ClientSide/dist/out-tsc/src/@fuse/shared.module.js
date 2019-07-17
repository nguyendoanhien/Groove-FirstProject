import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FuseDirectivesModule } from '@fuse/directives/directives';
import { FusePipesModule } from '@fuse/pipes/pipes.module';
let FuseSharedModule = class FuseSharedModule {
};
FuseSharedModule = tslib_1.__decorate([
    NgModule({
        imports: [
            CommonModule,
            FormsModule,
            ReactiveFormsModule,
            FlexLayoutModule,
            FuseDirectivesModule,
            FusePipesModule
        ],
        exports: [
            CommonModule,
            FormsModule,
            ReactiveFormsModule,
            FlexLayoutModule,
            FuseDirectivesModule,
            FusePipesModule
        ]
    })
], FuseSharedModule);
export { FuseSharedModule };
//# sourceMappingURL=shared.module.js.map