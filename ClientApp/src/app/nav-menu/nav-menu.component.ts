import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  userLogin;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut() {
    localStorage.removeItem("jwt");
  }
  
  isUserAuthenticated() {
    let token: string = localStorage.getItem("jwt");
    if (token != null) {
      this.userLogin = localStorage.getItem("userLogin");
      return true;
    }
    else {
      return false;
    }
  }
}