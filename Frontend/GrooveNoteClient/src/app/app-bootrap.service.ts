import { Injectable } from '@angular/core';
import { AuthService } from './core/auth/auth.service';
import { UserProfileService } from './core/identity/userprofile.service';

@Injectable({
  providedIn: 'root'
})
export class AppBootrapService {

  constructor(private userProfileSevice : UserProfileService) { }

  initializeApp() {
    this.userProfileSevice.loadStoredUserProfile();
  }
}
