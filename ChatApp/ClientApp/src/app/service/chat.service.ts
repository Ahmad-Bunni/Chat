import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  public data: string[] = ["Welcome to simple chat!"];

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
      this.data.push(message)
    });
  }

  public sendMessage = async (message) => {

    await this.hubConnection.invoke('SendMessage', message);

  }

}
