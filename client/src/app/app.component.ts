import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faBell } from '@fortawesome/free-solid-svg-icons'
<<<<<<< HEAD
import { AccountService } from './_services/account.service';
import { User } from './_modles/user';
=======
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  faBell = faBell
  title: string = 'Tinner !';
  users: any

  constructor(private http: HttpClient, private accountService: AccountService) { }

  setCurrentUser(){
    const userString = localStorage.getItem('user')
    if (userString=== null) return
    const user : User = JSON.parse(userString)
    this.accountService.setCurrentUser(user)
  }
  ngOnInit(): void {
    // this.getUser();  
    this.setCurrentUser()
  }
<<<<<<< HEAD

 
=======
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
}