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
import { VerticalLayout2Component } from 'app/layout/vertical/layout-2/layout-2.component';
let VerticalLayout2Module = class VerticalLayout2Module {
};
VerticalLayout2Module = tslib_1.__decorate([
    NgModule({
        declarations: [
            VerticalLayout2Component
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
            VerticalLayout2Component
        ]
    })
], VerticalLayout2Module);
export { VerticalLayout2Module };
//# sourceMappingURL=layout-2.module.js.map