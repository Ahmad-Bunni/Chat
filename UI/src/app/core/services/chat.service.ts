import { Message } from '@features/chat/models/message.model'
import { AuthenticationService } from '.'
import { Injectable } from '@angular/core'
import { Subject } from 'rxjs'
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'

@Injectable({
  providedIn: 'any',
})
export class ChatService {
  constructor(private authService: AuthenticationService) {}

  private messages: Message[] = []

  public observableMessages = new Subject<Message[]>()

  public observableUsers = new Subject<string[]>()

  public observableGroups = new Subject<string[]>()

  private hubConnection: HubConnection

  public startConnection = async () => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/MessageHub', {
        accessTokenFactory: () => this.authService.getToken(),
      })
      .build()

    await this.hubConnection
      .start()
      .then(() => console.log('connection started!'))
      .catch((err) => {
        console.log(err)
        throw err
      })
  }

  public addReceiveMessageListener = async () => {
    await this.hubConnection.on('Send', (message) => {
      this.messages.push(message)
      this.observableMessages.next(this.messages)
    })
  }

  public addReceiveUsersListener = async () => {
    await this.hubConnection.on('Users', (users) => {
      this.observableUsers.next(users)
    })
  }

  public sendMessage = async (message) => {
    await this.hubConnection.invoke('SendMessage', message)
  }

  public joinGroup = async (groupName) => {
    await this.hubConnection.invoke('JoinGroup', groupName)
  }
  public disconnect = async () => {
    await this.hubConnection.stop()
  }
}
