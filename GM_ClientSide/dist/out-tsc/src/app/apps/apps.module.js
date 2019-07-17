import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { AppsComponent } from './apps.component';
const routes = [
    {
        path: 'chat',
        loadChildren: './chat/chat.module#ChatModule'
    },
];
let AppsModule = class AppsModule {
};
AppsModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            AppsComponent
        ],
        imports: [
            RouterModule.forChild(routes),
            FuseSharedModule
        ]
    })
], AppsModule);
export { AppsModule };
//# sourceMappingURL=apps.module.js.map