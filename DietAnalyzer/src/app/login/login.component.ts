import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from '../auth/services/auth.service';
import { ApiUser } from '../common/interfaces/api-user.interface';
import { WelcomeDialogElement } from '../shared/dialogs/welcome-dialog/welcome-dialog';
import { WrongLoginDialogElement } from '../shared/dialogs/wrong-login-dialog/wrong-login-dialog';
import { ApiUsersService } from '../common/api/services/api.users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  logInForm !: FormGroup;
  registerForm !: FormGroup;

  currentUser !: ApiUser;

  hidePassword = true;

  isLogIn = true;
  isSignIn = false;
  remember = false;

  constructor(private authService: AuthService, private apiUsersService: ApiUsersService, private router: Router, public dialog: MatDialog) { }

  openWelcomeDialog() {
    this.dialog.open(WelcomeDialogElement);
  }

  openWrongLoginDialog() {
    this.dialog.open(WrongLoginDialogElement);
  }

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
    if (this.logInForm.valid) {
      const { email, password } = this.logInForm.value;
      
      this.authService.login(email, password, this.remember).subscribe(
        (res) => {
          if(res.id != null) {
            this.apiUsersService.getApiUserById(res.id).subscribe(user => {
              this.currentUser = user;

              window.location.reload();
            });

            this.router.navigate(['/']);
          }
          else if(res == null) {
            this.openWrongLoginDialog();
          }
        },
        (error) => {
          this.openWrongLoginDialog();
        }
      );
    }
  }

  onBtnCheckInFormClick() {

  }
}
