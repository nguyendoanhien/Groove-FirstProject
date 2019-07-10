import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule, MatProgressSpinnerModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import {ResetPasswordComponent} from './reset-password/reset-password.component';
import {MailConfirmComponent} from './mail-confirm/mail-confirm.component'
const matModules = [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule];


@NgModule({
  declarations: [LoginComponent,RegisterComponent,ResetPasswordComponent,ForgotPasswordComponent, UserProfileComponent, MailConfirmComponent],
  imports: [
    MatProgressSpinnerModule,
    CommonModule,
    AccountRoutingModule,
    matModules,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AccountModule { }
