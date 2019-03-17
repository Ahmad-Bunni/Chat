import { Component, OnInit } from '@angular/core';
import { ChatService } from '../home/service/chat.service';
import { Message } from '../home/model/message';
import { User } from './model/User';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  public messages: Message[];

  public users: User[] = [];

  public groups: string[] = [''];

  public nick: string;

  public message: string;

  public isReady: boolean = false;

  public constructor(public chatService: ChatService) { }

  async ngOnInit() {

    try {

      await this.chatService.startConnection();
      await this.chatService.addReceiveMessageListener();
      await this.chatService.addReceiveUsersListener();

      this.chatService.observableMessages.subscribe(msgs => this.messages = msgs);

      this.chatService.observableUsers.subscribe(users => this.users = users);

      this.isReady = true;
    } catch (error) {
      this.isReady = false;
    }
  }

  async Send() {

    if (this.isReady) {

      this.isReady = false;
      const messageObject = new Message();
      messageObject.content = this.message;
      messageObject.username = this.nick;

      await this.chatService.sendMessage(messageObject);

      this.message = null;
      this.isReady = true;
    }

  }

  isValid(): boolean {


    if (this.isReady && this.message && this.message.trim()) {
      return true;
    } else {
      return false;
    }
  }

}

