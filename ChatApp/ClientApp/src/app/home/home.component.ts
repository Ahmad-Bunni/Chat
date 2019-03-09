import { Component, OnInit } from '@angular/core';
import { ChatService } from '../service/chat.service';
import { Message } from '../model/message';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  private messages: string[];

  private users: string[] = [];
  
  private nick : string;

  private message: string;

  public constructor (public chatService : ChatService) { }

  async ngOnInit() {

    this.nick = window.prompt('Your name:', 'Hadi');

    await this.chatService.startConnection();
    await this.chatService.addReceiveMessageListener();   

    this.messages = this.chatService.data;
  }

  async Send() {

    const messageObject  = new Message();
    messageObject.content = this.message;
    messageObject.username = this.nick;
    
    await this.chatService.sendMessage(messageObject);

    this.message = null;

  }

}

