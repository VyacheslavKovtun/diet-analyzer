import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    authed : boolean = false;

    constructor(private authService: AuthService, private router: Router) {
        this.authService.isUserAuth$.subscribe(res => {
           this.authed = res; 
        });
     }

    canActivate() {
        if (this.authed) {
            return true;
        }
    
        this.router.navigate(['/login']);
        return false;
    }
}