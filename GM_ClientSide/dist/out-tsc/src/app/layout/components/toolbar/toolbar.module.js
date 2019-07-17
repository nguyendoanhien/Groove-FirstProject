import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';
import { ToolbarComponent } from 'app/layout/components/toolbar/toolbar.component';
let ToolbarModule = class ToolbarModule {
};
ToolbarModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            ToolbarComponent
        ],
        imports: [
            RouterModule,
            MatButtonModule,
            MatIconModule,
            MatMenuModule,
            MatToolbarModule,
            FuseSharedModule,
            FuseSearchBarModule,
            FuseShortcutsModule
        ],
        exports: [
            ToolbarComponent
        ]
    })
], ToolbarModule);
export { ToolbarModule };
//# sourceMappingURL=toolbar.module.js.map