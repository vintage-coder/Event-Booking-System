import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Router} from '@angular/router';





@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

 
  constructor(private http:HttpClient, private router:Router,
    ) { }


  getWebToken(credentials:any)
  {
    this.http.post("http://localhost:57589/ebs/v1/auth/login", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("webToken", token);
    
      this.router.navigate(["/dashboard"]);
    }, err => {
      console.log("Error message : not able to get jwt token");
    });

  }



}
