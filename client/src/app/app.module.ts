import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavComponent } from './nav/nav.component'
import { FormsModule } from '@angular/forms';
<<<<<<< HEAD
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { ToastrModule } from 'ngx-toastr';
=======
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670

@NgModule({
  declarations: [
    AppComponent,
<<<<<<< HEAD
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessagesComponent
=======
    NavComponent
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FontAwesomeModule,
<<<<<<< HEAD
    FormsModule,
    BsDropdownModule.forRoot(),
    BrowserAnimationsModule,
    ToastrModule.forRoot() //import { ToastrModule } from 'ngx-toastr'
=======
    FormsModule
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }