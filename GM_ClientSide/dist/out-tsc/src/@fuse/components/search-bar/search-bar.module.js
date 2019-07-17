import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FuseSearchBarComponent } from './search-bar.component';
let FuseSearchBarModule = class FuseSearchBarModule {
};
FuseSearchBarModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            FuseSearchBarComponent
        ],
        imports: [
            CommonModule,
            RouterModule,
            MatButtonModule,
            MatIconModule
        ],
        exports: [
            FuseSearchBarComponent
        ]
    })
], FuseSearchBarModule);
export { FuseSearchBarModule };
//# sourceMappingURL=search-bar.module.js.map