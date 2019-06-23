import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UserProfileService } from 'src/app/core/identity/userprofile.service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.css']
})
export class TopMenuComponent implements OnInit {
  public appTitle: string;
  public displayName: string;
  constructor(private userProfileService:  UserProfileService) {
    this.appTitle = environment.appName;
    this.displayName = "";
    this.userProfileService.displayNameSub$.subscribe((name: string) => {
      this.displayName = name;
    })

  }

  ngOnInit() {
  }

}
