import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Message } from './models/message';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GroupForm } from './models/group-form';
import { MessageForm } from './models/message-form';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private hubConnection: signalR.HubConnection;
  public messages: Message[] = [];
  public isGroupSet: boolean = false;

  groupForm = new FormGroup<GroupForm>({
    user: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required]
    }),
    group: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required]
    })
  });

  newMessageForm = new FormGroup<MessageForm>({
    message: new FormControl('', {
      nonNullable: true,
      validators: [Validators.required]
    })
  });

  ngOnInit() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7225/chat')
      .build();

    this.hubConnection.on('ReceiveMessage', (user: string, message: string) => {
      this.messages.push({ user: user, message: message});
    });

    this.hubConnection.start();
  }

  setGroup() : void {
    this.isGroupSet = true;

    this.hubConnection.invoke('JoinGroup', 
      this.groupForm.get('user')?.value,
      this.groupForm.get('group')?.value);
  }

  sendMessage(): void {
    this.hubConnection.invoke('SendMessage',
      this.groupForm.get('group')?.value,
      this.groupForm.get('user')?.value,
      this.newMessageForm.get('message')?.value
    );
  }
}
