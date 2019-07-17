import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';
import { ContentModule } from 'app/layout/components/content/content.module';
import { FooterModule } from 'app/layout/components/footer/footer.module';
import { NavbarModule } from 'app/layout/components/navbar/navbar.module';
import { QuickPanelModule } from 'app/layout/components/quick-panel/quick-panel.module';
import { ToolbarModule } from 'app/layout/components/toolbar/toolbar.module';
import { HorizontalLayout1Component } from 'app/layout/horizontal/layout-1/layout-1.component';
let HorizontalLayout1Module = class HorizontalLayout1Module {
};
HorizontalLayout1Module = tslib_1.__decorate([
    NgModule({
        declarations: [
            HorizontalLayout1Component
        ],
        imports: [
            MatSidenavModule,
            FuseSharedModule,
            FuseSidebarModule,
            FuseThemeOptionsModule,
            ContentModule,
            FooterModule,
            NavbarModule,
            QuickPanelModule,
            ToolbarModule
        ],
        exports: [
            HorizontalLayout1Component
        ]
    })
], HorizontalLayout1Module);
export { HorizontalLayout1Module };
//# sourceMappingURL=layout-1.module.js.map