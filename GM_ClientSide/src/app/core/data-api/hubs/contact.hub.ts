import { Injectable} from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { AuthService } from 'app/core/auth/auth.service';
import { BehaviorSubject} from 'rxjs';
import { ContactModel } from 'app/models/contact.model';
@Injectable({
    providedIn: 'root'
})
export class ContactHubService{

    public newContact: BehaviorSubject<Object>
    public removedContact: BehaviorSubject<ContactModel>
    public _hubConnection: signalR.HubConnection

    constructor(private authService: AuthService) {
        this.startConnection();
        this.newContact= new BehaviorSubject<Object>(null);
        this._hubConnection.on('SendNewContactToFriend', (contact: ContactModel,chatContact:any,dialog:any)=> {           
            var newContact = {contact:contact,chatContact:chatContact,dialog:dialog};        
            this.newContact.next(newContact);
        });
    }
    
    public startConnection = () => {
        const securityToken = this.authService.getToken();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44383/contacthub', { accessTokenFactory: () => securityToken })
            .build();

        this._hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }
}