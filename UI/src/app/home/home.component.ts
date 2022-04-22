import { Component, OnInit, OnDestroy } from '@angular/core';
import { ChatService } from '../service/chat.service';
import { Message } from '../model/message.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  public messages: Message[];

  public users: string[] = [];

  public groups: string[] = [''];

  public nick: string;

  public message: string;

  public isReady: boolean = false;

  public constructor(public chatService: ChatService) { }

  async ngOnDestroy() {
    await this.chatService.disconnect();
   }

   
  async ngOnInit() {

    try {

      await this.chatService.startConnection();
      await this.chatService.addReceiveMessageListener();
      await this.chatService.addReceiveUsersListener();
      this.chatService.observableMessages.subscribe(msgs => this.messages = msgs);
      this.chatService.observableUsers.subscribe(users => this.users = users);

      await this.chatService.joinGroup('Chaters');

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
      messageObject.groupName = 'Chaters';

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

