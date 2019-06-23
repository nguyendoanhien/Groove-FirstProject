import { Component, OnInit } from '@angular/core';
import { LoginModel } from './login.model';
import { UserProfileService } from 'src/app/core/identity/userprofile.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private login = new LoginModel("cuong.duongduy@gmail.com","Qwerty@123");

  constructor(private userProfileService: UserProfileService  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.userProfileService.logIn(this.login);
  }

}
