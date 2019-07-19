import { Injectable, OnInit } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { AuthService } from 'app/core/auth/auth.service';
import { MessageModel } from './../../../models/message.model';
import { BehaviorSubject} from 'rxjs';
@Injectable({
    providedIn: 'root'
})
export class ContactHubService implements OnInit {

    public newChatMessage: BehaviorSubject<MessageModel>
    public removedChatMessage: BehaviorSubject<MessageModel>
    public _hubConnection: signalR.HubConnection

    constructor(private authService: AuthService) {
        this.newChatMessage = new BehaviorSubject(null);
    }
    
    public startConnection = () => {
        const securityToken = this.authService.getToken();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44330/contactub', { accessTokenFactory: () => securityToken })
            .build();

        this._hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addAddNewContact(chatMessage: string, toUser: string) {
        this._hubConnection.invoke("AddNewContact", chatMessage, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
    public addAcceptFriend(chatMessageModel: MessageModel, toUser: string) {
        this._hubConnection.invoke("AcceptFriend", chatMessageModel, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
    
    ngOnInit() {
        this._hubConnection.on('SendNewContactToFriend', (message: MessageModel) => {
            this.newChatMessage.next(message);
        });
        this._hubConnection.on('SendRemoveContactToFriend', (message: MessageModel) => {
            this.removedChatMessage.next(message);
        });
    }
}