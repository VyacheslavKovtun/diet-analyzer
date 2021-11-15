import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  logInForm !: FormGroup;
  registerForm !: FormGroup;

  hidePassword = true;

  isLogIn = true;
  isSignIn = false;
  remember = false;

  constructor() { }

  ngOnInit(): void {
    this.logInForm = new FormGroup({
      "email": new FormControl('', [Validators.email, Validators.required]),
      "password": new FormControl('', Validators.required)
    });
    
    this.registerForm = new FormGroup({
      "userName": new FormControl('', Validators.required),
      "email": new FormControl('', [Validators.email, Validators.required]),
      "password": new FormControl('', Validators.required)
    });
  }

  loginSelected() {
    this.isLogIn = true;
    this.isSignIn = false;
  }

  signinSelected() {
    this.isSignIn = true;
    this.isLogIn = false;
  }

  onBtnLogInFormClick() {

  }

  onBtnCheckInFormClick() {

  }
}
