import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private jwtHelper: JwtHelperService, private router: Router) {
  }

  ngOnInit(): void {
    
  }

  isUserAuthenticated() {
    const token: string = localStorage.getItem("webToken");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }

 logOut()
  {
    localStorage.removeItem("webToken");
  }

}
