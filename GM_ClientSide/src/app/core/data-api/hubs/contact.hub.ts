import { Injectable, OnInit } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { AuthService } from 'app/core/auth/auth.service';
import { MessageModel } from './../../../models/message.model';
import { BehaviorSubject} from 'rxjs';
import { ContactModel } from 'app/models/contact.model';
@Injectable({
    providedIn: 'root'
})
export class ContactHubService implements OnInit {

    public newContact: BehaviorSubject<ContactModel>
    public removedContact: BehaviorSubject<ContactModel>
    public _hubConnection: signalR.HubConnection

    constructor(private authService: AuthService) {
        this.newContact= new BehaviorSubject(null);
    }
    
    public startConnection = () => {
        const securityToken = this.authService.getToken();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44330/contacthub', { accessTokenFactory: () => securityToken })
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
    public addAcceptFriend(replyMessage:string, toUser: string) {
        this._hubConnection.invoke("AcceptFriend", replyMessage, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
    
    ngOnInit() {
        this._hubConnection.on('SendNewContactToFriend', (contact: ContactModel) => {
            this.newContact.next(contact);
        });
        this._hubConnection.on('SendRemoveContactToFriend', (contact: ContactModel) => {
            this.removedContact.next(contact);
        });
    }
}