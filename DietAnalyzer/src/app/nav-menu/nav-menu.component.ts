import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  authed: boolean = false;

  constructor(private authService: AuthService) {
    this.authService.isUserAuth$.subscribe(res => {
      this.authed = res;
    });
  }

  ngOnInit(): void {
    
  }

  logoutClick() {
    this.authService.logout();
  }
}
