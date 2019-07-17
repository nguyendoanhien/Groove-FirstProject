import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FuseSharedModule } from '@fuse/shared.module';
import { QuickPanelComponent } from 'app/layout/components/quick-panel/quick-panel.component';
let QuickPanelModule = class QuickPanelModule {
};
QuickPanelModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            QuickPanelComponent
        ],
        imports: [
            MatDividerModule,
            MatListModule,
            MatSlideToggleModule,
            FuseSharedModule,
        ],
        exports: [
            QuickPanelComponent
        ]
    })
], QuickPanelModule);
export { QuickPanelModule };
//# sourceMappingURL=quick-panel.module.js.map