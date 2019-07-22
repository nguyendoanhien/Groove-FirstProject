import { Injectable, OnInit } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { AuthService } from 'app/core/auth/auth.service';
import { MessageModel } from './../../../models/message.model';
import { BehaviorSubject} from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class MessageHubService implements OnInit {

    public newChatMessage: BehaviorSubject<MessageModel>
    public removedChatMessage: BehaviorSubject<MessageModel>
    public _hubConnection: signalR.HubConnection

    constructor(private authService: AuthService) {
        this.newChatMessage = new BehaviorSubject(null);
    }
    
    public startConnection = () => {
        const securityToken = this.authService.getToken();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44330/messagehub', { accessTokenFactory: () => securityToken })
            .build();

        this._hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addSendMessageToUser(chatMessage: string, toUser: string) {
        this._hubConnection.invoke("SendMessageToUser", chatMessage, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
    public addSendRemovedMessageToUser(chatMessageModel: MessageModel, toUser: string) {
        this._hubConnection.invoke("SendRemovedMessageToUser", chatMessageModel, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
    
    ngOnInit() {
        this._hubConnection.on('SendMessage', (message: MessageModel) => {
            this.newChatMessage.next(message);
        });
        this._hubConnection.on('SendRemovedMessage', (message: MessageModel) => {
            this.removedChatMessage.next(message);
        });
    }
}