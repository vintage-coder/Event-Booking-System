import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Router} from '@angular/router';
import { environment } from './../../../environments/environment';
import {UserRegistrationDto} from './../../Dtos/UserRegistrationDto';
import {RegistrationResponseDto} from './../../Dtos/RegistrationResponseDto';
import {NotificationService} from './../notification.service';
import {ExternalAuthDto} from './../../Models/ExternalAuthDto';
import { Observable } from 'rxjs';




@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

 
  constructor(private http:HttpClient,
    private notification:NotificationService, private router:Router
    ) { }


  getWebToken(credentials:any)
  {
    this.http.post(`${environment.apiURL}`+"/api/v1/auth/login", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("webToken", token);
    this.notification.showSuccess("login sucessfully!","Sucesss");
      this.router.navigate(["/admin"]);
    }, err => {
      console.log("Error message : not able to get jwt token");
    });

  }


  public registerUser(body:UserRegistrationDto)
  {
   this.http.post(`${environment.apiURL}`+"/api/v1/auth/registration", body, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response =>{
      console.log("registered successfully...");
    },err =>{
      console.log("Error Occured");
    });
  }
  

  public validateExternalAuth(externalAuth: ExternalAuthDto):Observable<ExternalAuthDto>
  {
    
    return this.http.post<ExternalAuthDto>(`${environment.apiURL}`+'/api/v1/googleauth/externallogin', externalAuth)
    
  }





}
