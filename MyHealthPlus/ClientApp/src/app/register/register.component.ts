import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, Validator } from '@angular/forms';
import { UserService } from '../Service/user.service';
import { IUser } from 'src/app/interface/userInterface';
import { BooleanLiteral } from 'typescript';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() userData: IUser;

  userSubmitted: boolean;

  registrationForm: FormGroup;

  constructor(private userService: UserService,
    private router: Router,
    private fb: FormBuilder)
  { }

  ngOnInit(): void {
    this.createRegistrationForm();
  }


  createRegistrationForm() {
    this.registrationForm = this.fb.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      address: [null, Validators.required],
      phoneNumber: [null, Validators.required],
      password: [null, [Validators.required, Validators.minLength(8)]],
      confirmPassword: [null, Validators.required],

    }, { validators: this.passwordMatchingValidator });
  }

  userDataHandler() {
    var url = window.location.pathname;
    var roleId = url === '/sign-up' ? 3 : 2;


    return this.userData = {
      userId: 0,
      firstName: this.firstName.value,
      lastName: this.lastName.value,
      email: this.email.value,
      address: this.address.value,
      phoneNumber: this.phoneNumber.value,
      password: this.password.value,
      roldeId: roleId

    }
  }

  get firstName() {
    return this.registrationForm.get('firstName') as FormControl;
  }

  get lastName() {
    return this.registrationForm.get('lastName') as FormControl;
  }

  get email() {
    return this.registrationForm.get('email') as FormControl;
  }

  get address() {
    return this.registrationForm.get('address') as FormControl;
  }

  get phoneNumber() {
    return this.registrationForm.get('phoneNumber') as FormControl;
  }

  get password() {
    return this.registrationForm.get('password') as FormControl;
  }

  get confirmPassword() {
    return this.registrationForm.get('confirmPassword') as FormControl;
  }


  passwordMatchingValidator(fg: FormGroup): Validators {
    return fg.get('password').value === fg.get('confirmPassword').value ? null : { notmatched: true };
  }

  onSubmit() {
    this.userSubmitted = true;

    if (this.registrationForm.valid) {
      this.userService.insertUser(this.userDataHandler());

      this.router.navigate(['/login']);
    }
  }


}
