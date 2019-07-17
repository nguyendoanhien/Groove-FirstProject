import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FuseSidebarModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';
import { ContentModule } from 'app/layout/components/content/content.module';
import { FooterModule } from 'app/layout/components/footer/footer.module';
import { NavbarModule } from 'app/layout/components/navbar/navbar.module';
import { QuickPanelModule } from 'app/layout/components/quick-panel/quick-panel.module';
import { ToolbarModule } from 'app/layout/components/toolbar/toolbar.module';
import { VerticalLayout1Component } from 'app/layout/vertical/layout-1/layout-1.component';
let VerticalLayout1Module = class VerticalLayout1Module {
};
VerticalLayout1Module = tslib_1.__decorate([
    NgModule({
        declarations: [
            VerticalLayout1Component
        ],
        imports: [
            RouterModule,
            FuseSharedModule,
            FuseSidebarModule,
            ContentModule,
            FooterModule,
            NavbarModule,
            QuickPanelModule,
            ToolbarModule
        ],
        exports: [
            VerticalLayout1Component
        ]
    })
], VerticalLayout1Module);
export { VerticalLayout1Module };
//# sourceMappingURL=layout-1.module.js.map