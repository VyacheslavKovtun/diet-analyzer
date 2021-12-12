import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

import { Router } from "@angular/router";
import { environment } from '../../../environments/environment';
import { ApiUsersService } from "src/app/common/api/services/api.users.service";
import { ApiUser } from "src/app/common/interfaces/api-user.interface";

@Injectable()
export class AuthService{
  isUserAuth$ = new BehaviorSubject<boolean>(false);

  url = "http://localhost:4200";

  constructor(private httpClient: HttpClient, private router: Router, private apiUsersService: ApiUsersService) {
    this.authing();
  }

  getCurrentUser() {
    return this.httpClient.get<ApiUser>(
    [environment.API_URL, 'auth', 'current-user'].join('/'),
    {
      withCredentials: true
    });
  }

  register(userName: string, email: string, password: string) {
    return this.httpClient.post
    (
      [environment.API_URL, 'auth', 'register'].join('/'),
      {userName, email, password},
      {
        withCredentials: true
      }
    );
  }

  login(email: string, password: string, rememberMe: boolean) {
    var returnUrl = this.url;
    
    return this.httpClient.post<{ id: string, name: string}>
    (
      [environment.API_URL, 'auth', 'login'].join('/'),
      { email, password, rememberMe, returnUrl },
      {
        withCredentials: true
      }
    );
  }

  logout() {
    return this.httpClient.delete(
      [environment.API_URL, 'auth', 'logout'].join('/'),
      {
        withCredentials: true
      }).subscribe(u => {
      this.isUserAuth$.next(false);
      this.router.navigate(['/login']);
    });
  }

  authing() {
    this.httpClient.get<boolean>(
      [environment.API_URL, 'auth', 'isAuthed'].join('/'),
      {
        withCredentials: true
      }).subscribe(res => {
        this.isUserAuth$.next(res);
    });
  }
}