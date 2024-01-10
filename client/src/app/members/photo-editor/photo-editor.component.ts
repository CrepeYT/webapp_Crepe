import { Component, Input, OnInit } from '@angular/core';
import { faTrashCan, faStar } from '@fortawesome/free-regular-svg-icons';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { MembersService } from 'src/app/_services/members.service';
import { Photo } from 'src/app/_models/Photo';
import { faBan, faUpload } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
hasAnotherDropZoneOver: any;

fileOverAnother($event: any) {
throw new Error('Method not implemented.');
}
  faTrashCan = faTrashCan
  faStar = faStar
  @Input() member: Member | undefined
  uploader: FileUploader | undefined
  hasBaseDropZoneOver = false
  baseUrl = environment.apiUrl
  users: User | undefined | null
  constructor(private accountService: AccountService,private memberService :MembersService ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.users = user
    })
  }
faUpload=faUpload
faBan=faBan

  ngOnInit(): void {
    this.initUploader()
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver =e
  }
  initUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + '/users/add-image',
      authToken: 'Bearer ' + this.users?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024 //MB to bytes
    })
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }
    this.uploader.onSuccessItem = (item, response, status, header) => {
      if (response) {
        const photo = JSON.parse(response)
        this.member?.photos.push(photo)
        if (photo.isMain && this.users && this.member) {
          this.users.photoUrl = photo.url
          this.member.mainPhotoUrl = photo.url
          this.accountService.setCurrentUser(this.users)
        }
      }
    }
  }
  setMainPhoto(photo: Photo) {
    this.memberService.setMainPhoto(photo.id).subscribe({
      next: () => {
        if (this.users && this.member) {
          this.users.photoUrl = photo.url
          this.accountService.setCurrentUser(this.users)
          this.member.mainPhotoUrl = photo.url
          this.member.photos.map((p) => {
            p.isMain = false
            if (p.id === photo.id) p.isMain = true
          })
        }
      }
    })
  }
  deletePhoto(photoId: number) {
    this.memberService.deletePhoto(photoId).subscribe({
      next: () => {
        if (this.member) {
          
          this.member.photos = this.member.photos.filter(photo => photo.id !== photoId)
        }
      }
    })
  }
}
