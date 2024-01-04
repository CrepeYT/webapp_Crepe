import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  count = 0;
  constructor(private spinner: NgxSpinnerService) { }

  busy(name?: string | undefined) {
    this.count++
    this.spinner.show(name, {
      type: 'timer',
      bdColor: "rgba(0, 0, 0, 0.8)",
      color: "#fff"
    })
  }
  idle(name?: string | undefined) {
    this.count--
    if (this.count <= 0) {
      this.count = 0
      this.spinner.hide(name)
    }
  }
}