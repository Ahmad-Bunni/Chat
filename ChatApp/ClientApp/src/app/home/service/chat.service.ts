import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Message } from '../model/Message';
import { User } from '../model/User';
import { Observable, observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private messages: Message[] = [];

  public observableMessages = new Subject<Message[]>();

  public observableUsers = new Subject<User[]>();

  private hubConnection: signalR.HubConnection

  public startConnection = async () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/message")
      .build();

    await this.hubConnection.start().then(() => console.log('connection started!')).catch(err => {

      console.log(err);
      throw err
    });

  }

  public addReceiveMessageListener = async () => {
    await this.hubConnection.on('receiveMessage', (message) => {
      this.messages.push(message);
      this.observableMessages.next(this.messages);
    });
  }

  public addReceiveUsersListener = async () => {
    await this.hubConnection.on('receiveUsers', (users) => {
      this.observableUsers.next(users);
    });
  }

  public sendMessage = async (message) => {

    await this.hubConnection.invoke('SendMessage', message);

  }

}
