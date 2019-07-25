import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
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
import { ResetPasswordService } from './core/account/reset-password.service';
import { RegisterService } from './core/account/register.service';
import { MessageHubService } from './core/data-api/hubs/message.hub';
import { UserInfoService } from './core/account/userInfo.service';
import { UserContactService } from './core/account/user-contact.service';
import { MessageService } from './core/data-api/services/message.service';
import { ProfileHubService } from './core/data-api/hubs/profile.hub';
import { ContactHubService } from './core/data-api/hubs/contact.hub';

@NgModule({
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
        // AccountModule
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
        AuthRouteGuardService,
        RegisterService,
        UserInfoService,
        ResetPasswordService,
        UserContactService,       
        MessageService        
    ]

})
export class AppModule {
}
