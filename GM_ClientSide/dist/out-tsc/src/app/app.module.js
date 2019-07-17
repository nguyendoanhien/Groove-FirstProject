import * as tslib_1 from "tslib";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import { TranslateModule } from '@ngx-translate/core';
import 'hammerjs';
import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';
import { fuseConfig } from 'app/fuse-config';
import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
// services
import { ChatService } from './apps/chat/chat.service';
import { FakeDbService } from './fake-api/fake-api.service';
import { AppRoutingModule } from './app-routing.module';
import { TokenHttpInterceptor } from './core/auth/token.httpinterceptor';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { UserProfileService } from './core/identity/userprofile.service';
import { AuthService } from './core/auth/auth.service';
import { AuthRouteGuardService } from './core/auth/authrouteguard.service';
let AppModule = class AppModule {
};
AppModule = tslib_1.__decorate([
    NgModule({
        declarations: [
            AppComponent,
            PageNotFoundComponent
        ],
        imports: [
            BrowserModule,
            BrowserAnimationsModule,
            HttpClientModule,
            AppRoutingModule,
            TranslateModule.forRoot(),
            InMemoryWebApiModule.forRoot(FakeDbService, {
                delay: 0,
                passThruUnknownUrl: true
            }),
            // Material moment date module
            MatMomentDateModule,
            // Material
            MatButtonModule,
            MatIconModule,
            // Fuse modules
            FuseModule.forRoot(fuseConfig),
            FuseProgressBarModule,
            FuseSharedModule,
            FuseSidebarModule,
            FuseThemeOptionsModule,
            // App modules
            LayoutModule
        ],
        bootstrap: [
            AppComponent
        ],
        providers: [
            ChatService,
            {
                provide: HTTP_INTERCEPTORS,
                useClass: TokenHttpInterceptor,
                multi: true
            },
            UserProfileService,
            AuthService,
            AuthRouteGuardService
        ],
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map