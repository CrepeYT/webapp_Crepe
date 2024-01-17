import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  validationErrors: string[] | undefined;
  maxDate: Date = new Date();
  registerForm: FormGroup = new FormGroup({});
  constructor(
    private toaster: ToastrService,
    public accountService: AccountService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}
  initForm() {
    this.registerForm = this.formBuilder.group({
      aka: [null, Validators.required],
      gender: ['non-binary'],
      birthDate: [null, Validators.required],
      city: ['101', Validators.required],
      country: ['thailand', Validators.required],
      username: [null, Validators.required],
      password: [
        null,
        [Validators.required, Validators.minLength(4), Validators.maxLength(8)],
      ],
      confirmPassword: [
        null,
        [Validators.required, this.matchValue('password')],
      ],
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: (_) =>
        this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
    });
  }
  matchValue(matchTo: string): ValidatorFn {
    return (ctrl: AbstractControl) =>
      ctrl.value === ctrl.parent?.get(matchTo)?.value
        ? null
        : { notMatching: true };
  }
  ngOnInit(): void {
    this.initForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  @Input() usersFromHomeCpmponent: any;

  model: any = {};

  private dateOnly(date_string: string | undefined) {
    if (!date_string) return;
    const date = new Date(date_string);
    return new Date(
      date.setMinutes(date.getMinutes() - date.getTimezoneOffset())
    )
      .toISOString()
      .slice(0, 10);
  }

  register() {
    const birthDate = this.dateOnly(
      this.registerForm.controls['birthDate'].value
    );
    const registerData = { ...this.registerForm.value, birthDate };
    this.accountService.register(registerData).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/members');
      },
      error: (err) => {
        this.validationErrors = err;
      },
    });
  }

  @Output() isCancel = new EventEmitter();
  cancel() {
    this.isCancel.emit(true);
  }
}
