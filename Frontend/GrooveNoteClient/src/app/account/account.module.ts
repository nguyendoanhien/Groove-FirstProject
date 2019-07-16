import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule  } from '@angular/material';
import { FormsModule } from '@angular/forms';

const matModules = [MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule ];


@NgModule({
  declarations: [LoginComponent, UserProfileComponent],
  imports: [
    CommonModule,
    AccountRoutingModule,
    matModules,
    FormsModule
  ]
})
export class AccountModule { }
