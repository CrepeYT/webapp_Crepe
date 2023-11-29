import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  @Output() isCancel = new EventEmitter()

  constructor(private router : Router ,private accountService: AccountService,private toastr : ToastrService) {}

  model: any = {}

  register() {
    console.log(this.model)
    this.accountService.register(this.model).subscribe({
      error: (err: any) => console.log(err) ,
        next: (resp: any) => { this.router.navigateByUrl('/members')
          
          console.log(resp)
        }
      })
  }

  cancel() {
    this.isCancel.emit(true)
  }
}