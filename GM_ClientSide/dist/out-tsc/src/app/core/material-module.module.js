import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { MatButtonModule, MatMenuModule, MatToolbarModule, MatIconModule, MatCardModule, MatTreeModule, MatProgressBarModule } from '@angular/material';
let MaterialModule = class MaterialModule {
};
MaterialModule = tslib_1.__decorate([
    NgModule({
        imports: [
            MatButtonModule,
            MatMenuModule,
            MatToolbarModule,
            MatIconModule,
            MatCardModule,
            MatTreeModule,
            MatProgressBarModule
        ],
        exports: [
            MatButtonModule,
            MatMenuModule,
            MatToolbarModule,
            MatIconModule,
            MatCardModule,
            MatTreeModule
        ]
    })
], MaterialModule);
export { MaterialModule };
//# sourceMappingURL=material-module.module.js.map