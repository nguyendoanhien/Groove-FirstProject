import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FuseSidebarModule } from '@fuse/components/index';
import { FuseSharedModule } from '@fuse/shared.module';
import { ContentModule } from 'app/layout/components/content/content.module';
import { FooterModule } from 'app/layout/components/footer/footer.module';
import { NavbarModule } from 'app/layout/components/navbar/navbar.module';
import { QuickPanelModule } from 'app/layout/components/quick-panel/quick-panel.module';
import { ToolbarModule } from 'app/layout/components/toolbar/toolbar.module';
import { VerticalLayout3Component } from 'app/layout/vertical/layout-3/layout-3.component';
let VerticalLayout3Module = class VerticalLayout3Module {
};
VerticalLayout3Module = tslib_1.__decorate([
    NgModule({
        declarations: [
            VerticalLayout3Component
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
            VerticalLayout3Component
        ]
    })
], VerticalLayout3Module);
export { VerticalLayout3Module };
//# sourceMappingURL=layout-3.module.js.map