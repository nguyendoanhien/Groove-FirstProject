import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { FuseDemoContentComponent } from './demo-content/demo-content.component';
import { FuseDemoSidebarComponent } from './demo-sidebar/demo-sidebar.component';
let FuseDemoModule = class FuseDemoModule {
};
FuseDemoModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            FuseDemoContentComponent,
            FuseDemoSidebarComponent
        ],
        imports: [
            RouterModule,
            MatDividerModule,
            MatListModule
        ],
        exports: [
            FuseDemoContentComponent,
            FuseDemoSidebarComponent
        ]
    })
], FuseDemoModule);
export { FuseDemoModule };
//# sourceMappingURL=demo.module.js.map