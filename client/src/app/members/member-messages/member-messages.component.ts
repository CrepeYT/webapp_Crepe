import { CommonModule } from '@angular/common';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { faClock , faPaperPlane} from '@fortawesome/free-regular-svg-icons';
import { NgxLongPress2Module } from 'ngx-long-press2';
import { TimeagoModule } from 'ngx-timeago';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css'],
  standalone: true, //เพราะจะเอาไปใช้ใน member-detail.component ซึ่งเป็น component แบบ standalone
  imports: [CommonModule, FontAwesomeModule,TimeagoModule,FormsModule,NgxLongPress2Module],
 
})
export class MemberMessagesComponent implements OnInit {
  @Input() username?: string
  @Input() messages: Message[] = []
  faClock = faClock
  faPaperPlane = faPaperPlane
  @ViewChild('messageForm') messageForm?: NgForm
  messageContent = '' 
  constructor(private messageService: MessageService) { }

  loadMessages() {
    if (!this.username) return

    this.messageService.getMessagesThread(this.username).subscribe({
      next: response => this.messages = response
    })
  }

  ngOnInit(): void {
    this.loadMessages()
  }
  sendMessage() {
    if (!this.username) return
    this.messageService.sendMessage(this.username, this.messageContent).subscribe({
      next: response => {   this.messages.push(response)
                            this.messageForm?.reset()      }
    })
  }
  onLongPressMessage(id: number) {
    // console.log('delete me, id: ' + id)
    this.messageService.deleteMessage(id).subscribe({
      next: _ => this.messages?.splice(this.messages.findIndex(ms => ms.id === id), 1)
    })
  }
}