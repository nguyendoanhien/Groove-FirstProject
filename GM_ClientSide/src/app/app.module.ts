import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatMomentDateModule } from "@angular/material-moment-adapter";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { InMemoryWebApiModule } from "angular-in-memory-web-api";
import { TranslateModule } from "@ngx-translate/core";
import "hammerjs";

import { FuseModule } from "@fuse/fuse.module";
import { FuseSharedModule } from "@fuse/shared.module";
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from "@fuse/components";

import { fuseConfig } from "app/fuse-config";

import { AppComponent } from "app/app.component";
import { LayoutModule } from "app/layout/layout.module";
// services
import { ChatService } from "./apps/chat/chat.service";
import { FakeDbService } from "./fake-api/fake-api.service";
import { AppRoutingModule } from "./app-routing.module";
import { TokenHttpInterceptor } from "./core/auth/token.httpinterceptor";
import { PageNotFoundComponent } from "./pages/page-not-found/page-not-found.component";
import { UserProfileService } from "./core/identity/userprofile.service";
import { AuthService } from "./core/auth/auth.service";
import { AuthRouteGuardService } from "./core/auth/authrouteguard.service";
import { ResetPasswordService } from "./core/account/reset-password.service";
import { RegisterService } from "./core/account/register.service";
import { UserInfoService } from "./core/account/userInfo.service";
import { UserContactService } from "./core/account/user-contact.service";
import { MessageService } from "./core/data-api/services/message.service";
import {
    SpeechRecognitionModule,
    SpeechRecognitionLang,
    SpeechRecognitionMaxAlternatives,
    SpeechRecognitionService,
    RxSpeechRecognitionService
} from "@kamiazya/ngx-speech-recognition";
import { PickerModule } from '@ctrl/ngx-emoji-mart';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core'; // add this line
import { OpenGrapthService } from "./core/data-api/services/open-grapth.service";
import { AppHelperService } from "./core/utilities/app-helper.service";
import { FacebookModule, FacebookService } from "ngx-facebook";
import { CloudinaryModule } from "@cloudinary/angular-5.x";
import * as Cloudinary from "cloudinary-core";
import { ScrollEventModule } from 'ngx-scroll-event';
import { WindowRef } from '@fuse/services/window-ref';
import {
    SocialLoginModule,
    AuthServiceConfig,
    GoogleLoginProvider,
    FacebookLoginProvider
} from "angularx-social-login";
import { environment } from 'environments/environment';
import { CookieService } from 'ngx-cookie-service';
const config = new AuthServiceConfig([
    {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider(environment.applicationGoogle.clientId)
    },
    {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider(environment.applicationFacebook.appId)
    }
]);

export function provideConfig(): AuthServiceConfig {


    return config;
}
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
        FacebookModule.forRoot(),
        TranslateModule.forRoot(),
        SocialLoginModule,
        InMemoryWebApiModule.forRoot(FakeDbService,
            {
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
        LayoutModule,

        // AccountModule
        SpeechRecognitionModule.withConfig({
            lang: "en-US",
            interimResults: true,
            maxAlternatives: 10,
        }),
        PickerModule,
        CloudinaryModule.forRoot(Cloudinary,
            { cloud_name: "groovemessenger", upload_preset: "qlbjv3if", private_cdn: "true" }),
        ScrollEventModule
    ],
    bootstrap: [
        AppComponent
    ],
    providers: [
        CookieService, {
            provide: AuthServiceConfig,
            useFactory: provideConfig
        },
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
        MessageService,
        OpenGrapthService,
        AppHelperService,
        {
            provide: SpeechRecognitionLang,
            useValue: "en-US",
        },
        {
            provide: SpeechRecognitionMaxAlternatives,
            useValue: 1,
        },
        SpeechRecognitionService,
        RxSpeechRecognitionService,
        WindowRef,
        SpeechRecognition
    ],
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA,
        RxSpeechRecognitionService,
        FacebookService
    ]
})
export class AppModule {
}