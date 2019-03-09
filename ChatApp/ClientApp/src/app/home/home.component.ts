import { Component, OnInit } from '@angular/core';
import { ChatService } from '../service/chat.service';
import { Message } from '../model/message';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  public messages: string[];

  public users: string[] = [];

  public nick: string;

  public message: string;

  public isReady: boolean = false;

  public constructor(public chatService: ChatService) { }

  async ngOnInit() {

    this.nick = window.prompt('Your name:', 'Hadi');

    try {


      await this.chatService.startConnection();
      await this.chatService.addReceiveMessageListener();

      this.messages = this.chatService.data;

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

