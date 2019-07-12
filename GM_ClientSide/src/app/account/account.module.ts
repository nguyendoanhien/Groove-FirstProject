import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule, MatProgressSpinnerModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component'
import { CookieService } from 'ngx-cookie-service';
import { MailConfirmComponent } from './mail-confirm/mail-confirm.component';
import { MailConfirmerComponent } from './mail-confirmer/mail-confirmer.component';
import { ResetPasswordService } from 'app/services/reset-password.service';
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

  declarations: [LoginComponent, RegisterComponent, ResetPasswordComponent, ForgotPasswordComponent, UserProfileComponent, MailConfirmComponent, MailConfirmerComponent],
  imports: [
    MatProgressSpinnerModule,
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
  }, ResetPasswordService]


})
export class AccountModule { }
