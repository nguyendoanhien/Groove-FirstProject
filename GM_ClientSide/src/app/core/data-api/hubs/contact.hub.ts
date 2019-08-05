import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { AuthService } from "app/core/auth/auth.service";
import { BehaviorSubject } from "rxjs";
import { ContactModel } from "app/models/contact.model";
import { environment } from "environments/environment";

@Injectable()
export class ContactHubService {

    newContact: BehaviorSubject<Object>;
    removedContact: BehaviorSubject<ContactModel>;
    newGroup : BehaviorSubject<Object>;
    editGroup:BehaviorSubject<Object>;
    _hubConnection: signalR.HubConnection;

    constructor(private authService: AuthService) {
        this.startConnection();
        this.newContact = new BehaviorSubject<Object>(null);
        this.newGroup = new BehaviorSubject<Object>(null);
        this.editGroup = new BehaviorSubject<Object>(null);
        this._hubConnection.on("SendNewContactToFriend",
            (contact: ContactModel, chatContact: any, dialog: any) => {
                var newContact = { contact: contact, chatContact: chatContact, dialog: dialog };
                this.newContact.next(newContact);
            });
        this._hubConnection.on("BroadcastNewGroupToFriends", (newGroupInfo) => this.newGroup.next(newGroupInfo));
        this._hubConnection.on("EditConversationToFriends", (editConversation) => this.editGroup.next(editConversation));
    }

    startConnection = () => {
        const securityToken = this.authService.getToken();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.hub.contactUrl, { accessTokenFactory: () => securityToken })
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = environment.hub.serverTimeoutInSeconds * 1000;
        this._hubConnection
            .start()
            .then(() => console.log("[Contact Hub]: Connection started"))
            .catch(err => console.log(`[Contact Hub]: Error while starting connection: ${err}`));
    };
}