import { Component, OnInit } from '@angular/core'
import { AccountService } from '../_services/account.service'
<<<<<<< HEAD
import { Observable, of } from 'rxjs'
import { User } from '../_modles/user'
import { Router } from '@angular/router'
import { ToastrService } from 'ngx-toastr'
=======
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
<<<<<<< HEAD
export class NavComponent implements OnInit {
  model: any = {}
  currentUser$: Observable<User | null> = of(null) // isLogin = false

  constructor(private toastr: ToastrService, private router: Router, public accountService: AccountService){ }

  
  ngOnInit(): void {
      this.currentUser$ = this.accountService.currentUser$
  }
  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
        // next: user => this.isLogin = !!user, // user?true:false
        error: err => console.log(err)
    })
}
login(): void {
   this.accountService.login(this.model).subscribe({
          next: _ => this.router.navigateByUrl('/members'),
          error: err => this.toastr.error(err.error)
  })
}
logout() {
  this.accountService.logout()
  this.router.navigateByUrl('/')
}
}
=======
export class NavComponent {
  model: { username: string | undefined, password: string | undefined } = {
      username: undefined,
      password: undefined
  }
  isLogin = false

  constructor(private accountService: AccountService) { }

  login(): void {
      this.accountService.login(this.model).subscribe({ //Observable
          next: response => {
              console.log(response)
              this.isLogin = true
          },
          error: err => console.log(err) //anything that's not in 200 range of HTTP status
      })
  }
}

>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
