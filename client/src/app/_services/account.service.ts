import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
<<<<<<< HEAD
import { User } from '../_modles/user';
import { BehaviorSubject, map } from 'rxjs';

=======
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    baseUrl = 'https://localhost:7777/api/'
<<<<<<< HEAD
    private currentUserSource = new BehaviorSubject<User | null>(null)
    currentUser$ = this.currentUserSource.asObservable()//the $ is convention to signify that this is observable
  register: any;
=======
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670

    constructor(private http: HttpClient) { }

    login(model: any) {
<<<<<<< HEAD
        return this.http.post<User>(`${this.baseUrl}account/login`, model).pipe(
        map((user : User) => {
            if (user){
                localStorage.setItem('user',JSON.stringify(user))
                this.currentUserSource.next
            }

}
        ))
    }
    logout() {
        localStorage.removeItem('user')
        this.currentUserSource.next(null)
    }
    setCurrentUser(user: User) {
        this.currentUserSource.next(user)
    }
    
=======
        return this.http.post(`${this.baseUrl}account/login`, model)
    }
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
}