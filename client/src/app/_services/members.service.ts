import { HttpClient, HttpHeaders } from '@angular/common/http'
import { environment } from 'src/environments/environment'
import { Member } from '../_modules/member'
import { Injectable } from '@angular/core'
import { map, of } from 'rxjs'



@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl
  member:Member[]=[]

  constructor(private http: HttpClient) { }
  


  // getHttpOptions() {
  //   const userString = localStorage.getItem('user')
  //   if (!userString) return
  //   const user: User = JSON.parse(userString)
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token
  //     })
  //   }
  // }

  getMembers() {
    if (this.member.length > 0) return of(this.member)
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(users => {
        this.member = users
        return users
      })
    )
  }

  getMember(username: string) {
    const member = this.member.find(user => user.userName === username)
    if (member) return of(member)
    return this.http.get<Member>(this.baseUrl + 'users/username/' + username)
  }
  updateProfile(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(_ => {
        const index = this.member.indexOf(member)
        this.member[index] = { ...this.member[index], ...member }
      })
    )
  }
}
