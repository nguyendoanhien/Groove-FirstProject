import { Injectable, OnInit } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { AuthService } from 'app/core/auth/auth.service';
import { BehaviorSubject } from 'rxjs';
import { UserInfo } from 'app/apps/chat/sidenavs/left/user/userInfo.model';

@Injectable({
    providedIn: 'root'
})
export class ProfileHubService implements OnInit {

    public newUserInfo: BehaviorSubject<UserInfo>
    public _hubConnection: signalR.HubConnection
    constructor(private authService: AuthService) {
        this.newUserInfo = new BehaviorSubject(null);

        const securityToken = this.authService.getToken();

        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44383/profileHub', { accessTokenFactory: () => securityToken })
            .build();

        this._hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))

    }
    ngOnInit() {

        this._hubConnection.on('UserProfile', (userInfo: UserInfo) => {
            console.log('123')
            console.log(userInfo);
        });

    }

    public addChangeUserProfile( userInfo: UserInfo) {
        this._hubConnection.invoke("ChangeUserProfile", userInfo).catch(function (err) {
            return console.error(err.toString());
        });
    }
}
