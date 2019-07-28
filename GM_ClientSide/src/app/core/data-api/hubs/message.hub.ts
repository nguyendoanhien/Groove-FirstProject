
import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { AuthService } from 'app/core/auth/auth.service';
import { MessageModel } from './../../../models/message.model';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'environments/environment';
import { UnreadMessage } from 'app/models/UnreadMessage.model';
@Injectable({
    providedIn: 'root'
})
export class MessageHubService {

    public newChatMessage: BehaviorSubject<MessageModel>
    public removedChatMessage: BehaviorSubject<MessageModel>
    public _hubConnection: signalR.HubConnection
    public unreadMessage: BehaviorSubject<UnreadMessage>

    constructor(private authService: AuthService) {
        this.newChatMessage = new BehaviorSubject(null);
        this.unreadMessage = new BehaviorSubject(null);
        this.startConnection();
        this._hubConnection.on('SendMessage', (message: MessageModel) => {
            this.newChatMessage.next(message);
        });
        this._hubConnection.on('SendRemovedMessage', (message: MessageModel) => {
            this.removedChatMessage.next(message);
        });
        this._hubConnection.on('SendUnreadMessagesAmount', (message: UnreadMessage) => {
            this.unreadMessage.next(message);
        });
    }

    public startConnection = () => {
        const securityToken = this.authService.getToken();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.messageHubUrl, { accessTokenFactory: () => securityToken })
            .build();

        this._hubConnection
            .start()
            .then(() => console.log('[Message Hub - ]Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addSendMessageToUser(message: MessageModel, toUser: string) {
        this._hubConnection.invoke("SendMessageToUser", message, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
    public addSendRemovedMessageToUser(chatMessageModel: MessageModel, toUser: string) {
        this._hubConnection.invoke("SendRemovedMessageToUser", chatMessageModel, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
}