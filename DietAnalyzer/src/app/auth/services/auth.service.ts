import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, of } from "rxjs";

import { tap } from 'rxjs/operators';
import { Router } from "@angular/router";
import { environment } from '../../../environments/environment';


@Injectable()
export class AuthService {
  url = "http://localhost:4200";

  constructor(private httpClient: HttpClient, private router: Router) {
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
      this.router.navigate(['/login']);
    });
  }

  isUserAuth(): boolean {
    
    return false;
  }
}