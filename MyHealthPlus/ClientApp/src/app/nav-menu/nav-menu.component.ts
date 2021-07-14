import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models';
import { AuthenticationService } from '../service/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  currentUser: User;
  isExpanded = false;
  roleId?: number;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);

    if (this.currentUser != null)
          this.roleId = this.currentUser.roleId;
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
