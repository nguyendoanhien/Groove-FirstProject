
import * as signalR from "@aspnet/signalr";
import { HubConnection, LogLevel } from "@aspnet/signalr";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";
import { AuthService } from "app/core/auth/auth.service";
import { BehaviorSubject } from "rxjs";
import { UserInfo } from "app/apps/chat/sidenavs/left/user/userInfo.model";

const profileHubUrl = environment.hub.profileUrl;
const profileHubLogLevel = LogLevel.Information;

@Injectable()
export class ProfileHubService {

    private _hubConnection: HubConnection;
    UserProfileChanged: BehaviorSubject<UserInfo>;

    constructor(private _authService: AuthService) {
        this.UserProfileChanged = new BehaviorSubject(null);
        this.initializeHub();
    }

    initializeHub(): void {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(profileHubUrl, { accessTokenFactory: () => this._authService.getToken() })
            .configureLogging(profileHubLogLevel)
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = environment.hub.serverTimeoutInSeconds * 1000;
        this._hubConnection
            .start()
            .then(() => console.log("[Profile Hub]: Connection started"))
            .catch(err => console.log(`[Profile Hub]: Error while starting connection: ${err}`));

        this.registerClientHandlers();
    }

    private registerClientHandlers(): void {
        this._hubConnection.on("ClientChangeUserProfile",
            (data: UserInfo) => {
                this.UserProfileChanged.next(data);
            });
    }

}