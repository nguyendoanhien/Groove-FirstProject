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
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ResetPasswordService } from 'app/services/reset-password.service';
const matModules = [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatCheckboxModule];


@NgModule({
    declarations: [LoginComponent, RegisterComponent, ResetPasswordComponent, ForgotPasswordComponent, UserProfileComponent],
    imports: [
        CommonModule,
        AccountRoutingModule,
        matModules,
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [ResetPasswordService]
})
export class AccountModule { }
