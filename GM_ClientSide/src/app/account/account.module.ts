import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component'
import { CookieService } from 'ngx-cookie-service';

import {
  SocialLoginModule,
  AuthServiceConfig,
  GoogleLoginProvider,
  FacebookLoginProvider
} from 'angularx-social-login';
const matModules = [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule];
let config = new AuthServiceConfig([
  {
    id: GoogleLoginProvider.PROVIDER_ID,
    provider: new GoogleLoginProvider("687824117544-nvc2uojbm14hc330gl8qh3lsrtl3tc4a.apps.googleusercontent.com")
  },
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("354060818601401")
  }
]);
export function provideConfig() {

  return config;
}

@NgModule({
  declarations: [LoginComponent, RegisterComponent, ResetPasswordComponent, ForgotPasswordComponent, UserProfileComponent],
  imports: [
    CommonModule,
    AccountRoutingModule,
    matModules,
    FormsModule,
    ReactiveFormsModule,
    SocialLoginModule
  ],
  providers: [CookieService, {
    provide: AuthServiceConfig,
    useFactory: provideConfig
  }]

})
export class AccountModule { }
