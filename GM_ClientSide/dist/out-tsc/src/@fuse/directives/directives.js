import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { FuseIfOnDomDirective } from '@fuse/directives/fuse-if-on-dom/fuse-if-on-dom.directive';
import { FuseInnerScrollDirective } from '@fuse/directives/fuse-inner-scroll/fuse-inner-scroll.directive';
import { FusePerfectScrollbarDirective } from '@fuse/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive';
import { FuseMatSidenavHelperDirective, FuseMatSidenavTogglerDirective } from '@fuse/directives/fuse-mat-sidenav/fuse-mat-sidenav.directive';
let FuseDirectivesModule = class FuseDirectivesModule {
};
FuseDirectivesModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            FuseIfOnDomDirective,
            FuseInnerScrollDirective,
            FuseMatSidenavHelperDirective,
            FuseMatSidenavTogglerDirective,
            FusePerfectScrollbarDirective
        ],
        imports: [],
        exports: [
            FuseIfOnDomDirective,
            FuseInnerScrollDirective,
            FuseMatSidenavHelperDirective,
            FuseMatSidenavTogglerDirective,
            FusePerfectScrollbarDirective
        ]
    })
], FuseDirectivesModule);
export { FuseDirectivesModule };
//# sourceMappingURL=directives.js.map