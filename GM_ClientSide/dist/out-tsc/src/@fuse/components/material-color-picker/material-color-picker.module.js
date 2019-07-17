import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FusePipesModule } from '@fuse/pipes/pipes.module';
import { FuseMaterialColorPickerComponent } from '@fuse/components/material-color-picker/material-color-picker.component';
let FuseMaterialColorPickerModule = class FuseMaterialColorPickerModule {
};
FuseMaterialColorPickerModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            FuseMaterialColorPickerComponent
        ],
        imports: [
            CommonModule,
            FlexLayoutModule,
            MatButtonModule,
            MatIconModule,
            MatMenuModule,
            MatTooltipModule,
            FusePipesModule
        ],
        exports: [
            FuseMaterialColorPickerComponent
        ],
    })
], FuseMaterialColorPickerModule);
export { FuseMaterialColorPickerModule };
//# sourceMappingURL=material-color-picker.module.js.map