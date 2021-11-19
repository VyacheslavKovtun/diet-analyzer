import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AuthService } from '../auth/services/auth.service';
import { ApiUser } from '../common/interfaces/api-user.interface';
import { DialogElement } from '../shared/dialogs/dialog';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  logInForm !: FormGroup;
  registerForm !: FormGroup;

  currentUser !: ApiUser;

  dialogElement!: DialogElement;

  hidePassword = true;

  isLogIn = true;
  isSignIn = false;

  constructor(private authService: AuthService, private router: Router, public dialog: MatDialog) { }

  openDialog(title: string, content: string, closeBtnTitle: string) {
    this.dialogElement.setDialog(title, content, closeBtnTitle);
    this.dialog.open(DialogElement);
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
      
      this.authService.login(email, password).subscribe((res) => {
        if(res == HttpStatusCode.Ok) {
          //TODO: replace "id" by ApiUser id
          localStorage.setItem("current_user", "id");

          this.openDialog("You are welcome!", "Welcome to EDA", "Close");

          this.router.navigate(['/']);
        }
        else if(res == HttpStatusCode.BadRequest) {
          this.openDialog("Error", "Wrong login or password", "Try again");
        }
      });
    }
  }

  onBtnCheckInFormClick() {

  }
}
