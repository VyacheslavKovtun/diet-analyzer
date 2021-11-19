import { HttpClient } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";

import { Router } from "@angular/router";
import { environment } from '../../../environments/environment';
import { ApiUsersService } from "src/app/common/api/services/api.users.service";

@Injectable()
export class AuthService{
  isUserAuth$ = new BehaviorSubject<boolean>(false);

  url = "http://localhost:4200";

  constructor(private httpClient: HttpClient, private router: Router, private apiUsersService: ApiUsersService) {
    var isAuthed = this.isUserAuth();
    this.isUserAuth$.next(isAuthed);
  }

  register(userName:string, email: string, password: string) {
    return this.httpClient.post
    (
      [environment.API_URL, 'auth', 'register'].join('/'),
      {userName, email, password}
    );
  }

  login(email: string, password: string) {
    var returnUrl = this.url;
    
    return this.httpClient.post<{ id: string, name: string}>
    (
      [environment.API_URL, 'auth', 'login'].join('/'),
      { email, password, returnUrl }
    );
  }

  logout() {
    return this.httpClient.delete([environment.API_URL, 'auth', 'logout'].join('/')).subscribe(u => {
      localStorage.removeItem("current_user");
      this.isUserAuth$.next(false);
      this.router.navigate(['/login']);
    });
  }

  isUserAuth(): boolean {
    var id = localStorage.getItem("current_user");

    if(id != null) {
      return true;
    }
    else {
      return false;
    }
  }
}