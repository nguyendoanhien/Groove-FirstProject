import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
const routes = [
    {
        path: 'account',
        loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
    },
    {
        path: 'apps',
        loadChildren: './apps/apps.module#AppsModule'
    },
    // {
    //     path      : '**',
    //     redirectTo: './apps/apps.module#AppsModule'
    // },
    { path: '**', component: PageNotFoundComponent }
];
let AppRoutingModule = class AppRoutingModule {
};
AppRoutingModule = tslib_1.__decorate([
    NgModule({
        imports: [RouterModule.forRoot(routes)],
        exports: [RouterModule]
    })
], AppRoutingModule);
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map