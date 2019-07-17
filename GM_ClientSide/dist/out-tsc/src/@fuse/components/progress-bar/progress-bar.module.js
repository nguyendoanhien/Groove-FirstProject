import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FuseProgressBarComponent } from './progress-bar.component';
let FuseProgressBarModule = class FuseProgressBarModule {
};
FuseProgressBarModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            FuseProgressBarComponent
        ],
        imports: [
            CommonModule,
            RouterModule,
            MatButtonModule,
            MatIconModule,
            MatProgressBarModule
        ],
        exports: [
            FuseProgressBarComponent
        ]
    })
], FuseProgressBarModule);
export { FuseProgressBarModule };
//# sourceMappingURL=progress-bar.module.js.map