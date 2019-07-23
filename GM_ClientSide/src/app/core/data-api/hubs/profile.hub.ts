import * as signalR from '@aspnet/signalr';
import { HubConnection, LogLevel } from '@aspnet/signalr';
import { Injectable, OnInit } from '@angular/core';
import { environment } from 'environments/environment';
import { AuthService } from 'app/core/auth/auth.service';
import { UserProfileModel } from 'app/account/user-profile/user-profile.model';
import { BehaviorSubject } from 'rxjs';

const profileHubUrl = environment.hub.profileUrl;
const profileHubLogLevel = signalR.LogLevel.Information;

@Injectable()
export class ProfileHubService {

    private _hubConnection: HubConnection;
    public UserProfileChanged: BehaviorSubject<UserProfileModel>;

    constructor(private _authService: AuthService) {
        this.UserProfileChanged = new BehaviorSubject(null);
        this.initializeHub();
    }

    public initializeHub(): void {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(profileHubUrl, { accessTokenFactory: () => this._authService.getToken() })
            .configureLogging(profileHubLogLevel)
            .build();

        this._hubConnection
            .start()
            .then(() => console.log('[Profile Hub]: Connection started'))
            .catch(err => console.log('[Profile Hub]: Error while starting connection: ' + err));

        this.registerClientHandlers();
    }

    private registerClientHandlers(): void {
        this._hubConnection.on('ClientChangeUserProfile', (data: UserProfileModel) => {
            console.log(data);
            this.UserProfileChanged.next(data);
        });
    }
}
