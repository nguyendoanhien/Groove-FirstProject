import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AuthRouteGuardService } from '../core/auth/authrouteguard.service';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { MailConfirmComponent } from './mail-confirm/mail-confirm.component';
import {MailConfirmerComponent} from './mail-confirmer/mail-confirmer.component'
const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  {path:'mail-confirmation', component: MailConfirmComponent},
  {path:'confirm', component: MailConfirmerComponent},
  { path: 'profile', component: UserProfileComponent, canActivate: [AuthRouteGuardService] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
