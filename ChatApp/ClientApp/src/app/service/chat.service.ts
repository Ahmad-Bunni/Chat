import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Message } from '../model/message.model';
import {Subject } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private authService : AuthService){}

  private messages: Message[] = [];

  public observableMessages = new Subject<Message[]>();

  public observableUsers = new Subject<string[]>();

  public observableGroups = new Subject<string[]>();

  private hubConnection: signalR.HubConnection

  public startConnection = async () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/MessageHub", { accessTokenFactory: () => this.authService.getToken() })
      .build();

    await this.hubConnection.start().then(() => console.log('connection started!')).catch(err => {

      console.log(err);
      throw err
    });

  }

  public addReceiveMessageListener = async () => {
  
    await this.hubConnection.on('Send', (message) => {
      this.messages.push(message);
      this.observableMessages.next(this.messages);
    });
  }

  public addReceiveUsersListener = async () => {
    await this.hubConnection.on('Users', (users) => {
      this.observableUsers.next(users);
    });
  }

  public sendMessage = async (message) => {
    await this.hubConnection.invoke('SendMessage', message);
  }

  public joinGroup = async (groupName) => {

    await this.hubConnection.invoke('JoinGroup', groupName);

  }
  public disconnect = async () => {

    await this.hubConnection.stop();
  }
}
