import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { ChatMessageModel } from '../chat-message.model';

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.css']
})
export class ChatboxComponent implements OnInit {
  private _hubConnection: HubConnection | undefined;

  fromUser: string = '';
  toUser: string = '';
  groupName: string = '';

  newMessage = '';
  messages: ChatMessageModel[] = [];

  constructor() { }

  public joinToHub(): void {
    this._hubConnection.invoke('Join', this.fromUser);
  }

  public connectToGroup(): void {
    this._hubConnection.invoke('JoinGroup', this.groupName);
  }

  public sendToGroup(): void {

    const newChatMessage: ChatMessageModel = {
      from: this.fromUser,
      payload: this.newMessage
    };


    if (this._hubConnection) {
      this._hubConnection.invoke('SendMessageToGroup', newChatMessage, this.groupName);
    }
    this.messages.push(newChatMessage);
  }

  public sendToUser(): void {

    const newChatMessage: ChatMessageModel = {
      from: this.fromUser,
      payload: this.newMessage
    };


    if (this._hubConnection) {
      this._hubConnection.invoke('SendMessageToUser', newChatMessage, this.toUser);
    }
    this.messages.push(newChatMessage);
  }

  ngOnInit() {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44383/chatHub')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this._hubConnection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('ReceiveMessage', (message: ChatMessageModel) => {
      this.messages.push(message);
    });

    this._hubConnection.on('BroadcastMessage', (message: ChatMessageModel) => {
      this.messages.push(message);
    });
  }

}
