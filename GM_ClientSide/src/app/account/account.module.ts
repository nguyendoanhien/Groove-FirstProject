import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule, MatProgressSpinnerModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
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
import { environment } from 'environments/environment';

const matModules = [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule];
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
