import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import {Route, Router} from '@angular/router';
import {NgForm}  from '@angular/forms';
import {CustomValidationService} from './../../Services/custom-validation.service';
import {AuthenticationService} from '../../Services/Authentication/authentication.service';
import {FacebookLoginProvider,GoogleLoginProvider, SocialAuthService} from 'angularx-social-login';
import {ExternalAuthDto} from './../../Models/ExternalAuthDto';

// import { SocialAuthService } from "angularx-social-login";
import { SocialUser } from "angularx-social-login";
// import { GoogleLoginProvider } from "angularx-social-login";

import {environment} from './../../../environments/environment';
import {NotificationService} from './../../Services/notification.service';
import { ExternalResDto } from 'src/app/Models/ExternalResDto';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup;
  submitted = false;
  showError:boolean;

  public selectedValue:string;

  public userRoles:string[]=["Admin","User"];


  public user: SocialUser = new SocialUser;
  constructor(private fb:FormBuilder, private customValidator:CustomValidationService,
     private http:HttpClient,
     private router:Router,private authenticationService:AuthenticationService,
     private socialAuthService: SocialAuthService,private notification:NotificationService
     ) {

  }

  ngOnInit(): void {

    this.loginForm=this.fb.group({
      name:[''],
      password:['',Validators.compose([Validators.required, this.customValidator.patternValidator()])],
      //confirmPassword:['', [Validators.required]]
    },
    // {
    //   validator: this.customValidator.MatchPassword('password', 'confirmPassword'),
    // }
    );


    this.socialAuthService.authState.subscribe(user => {
      this.user = user;
      console.log(user);
    });
  }

  get loginFormControl() {
    return this.loginForm.controls;
  }

    onLogin()
    {

      this.submitted=true;
      if(this.loginForm.valid)
      {
        console.log("Login was called...");
        alert('Form Submitted succesfully!!!\n Check the values in browser console.')
        console.log("name : "+this.loginForm.value.name);
        console.log("password : "+this.loginForm.value.password);
        //console.log("confirm password : "+this.loginForm.value.confirmPassword);
        console.table(this.loginForm.value);

        const credentials = JSON.stringify({ "username": this.loginForm.value.name, "password": this.loginForm.value.password })

        this.authenticationService.getWebToken(credentials);
      }


    }


     externalGoogleLogin()
    {
      console.log("External login was called ......");

    

      this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID)

      .then(res =>{
          const user:SocialUser={...res};
          console.log(user);

          // const externalAuth: ExternalAuthDto = {
          //   provider:user.provider,
          //   idToken:user.idToken
          // };

          // const externalAuth1 = {
          //   provider:user.provider,
          //   idToken:user.idToken
          // };
          console.log("=====================================");

          // console.log("external auth: "+externalAuth);

          // const body=JSON.stringify(externalAuth);

          // console.log("body: " + body);

          // console.log("External auth id token :"+externalAuth.idToken);

          // console.log("External auth provider :"+externalAuth.provider);
        
          let headers = new HttpHeaders({
            'Content-Type': 'application/json'
            });
          let options = { headers: headers };

        //   const hello={
        //     "provider":"GOOGLE",
        //   "idToken":"eyJhbGciOiJSUzI1NiIsImtpZCI6ImYxMzM4Y2EyNjgzNTg2M2Y2NzE0MDhmNDE3MzhhN2I0OWU3NDBmYzAiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXpwIjoiNDczNjQ1NDIxMjg4LTE5MnIyZmphaGdzaGJwMnM0aXNxdGE0bTU5anJscDNqLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiYXVkIjoiNDczNjQ1NDIxMjg4LTE5MnIyZmphaGdzaGJwMnM0aXNxdGE0bTU5anJscDNqLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwic3ViIjoiMTEwMDUzNzgwNTAxMjE5ODA0OTk2IiwiZW1haWwiOiJnb3d0aGFtYW52YXN1ZGV2QGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJhdF9oYXNoIjoiLWVvMG5TR19mVXVhVHhrQnRIY3hfdyIsIm5hbWUiOiJHT1dUSEFNQU4gVkFJS1VOREFOVEhBTiIsInBpY3R1cmUiOiJodHRwczovL2xoMy5nb29nbGV1c2VyY29udGVudC5jb20vYS0vQU9oMTRHaDFsX3NUeGhGMGxiWjczcmQwUnhkZEw3LVc5THpWUDBWeFVROWk9czk2LWMiLCJnaXZlbl9uYW1lIjoiR09XVEhBTUFOIiwiZmFtaWx5X25hbWUiOiJWQUlLVU5EQU5USEFOIiwibG9jYWxlIjoiZW4iLCJpYXQiOjE2NDk1NjEzNTMsImV4cCI6MTY0OTU2NDk1MywianRpIjoiODdiYTAyYjk3YzJjZWIyYzcyNGUzMTQ2ZDk1ZjcxNmFjNmY5NWJhYSJ9.XhXTY12DHNEAISZQ9Py8uyjfhZl8VXH_OZbUX4JSJJ_LvgsIxzoBNtSTeR0QWyTx1D6nUGYy696HSC0fA7V-0DUESiC9uLCCkvu-D5Spfo3rTKNiWXiEw7r3zgdLUu7H3OHAFXLwv-Zhh088O62AzkQt3S4HcCgpGdALOf44zXcUP1alLXHyjQnbSWT2q4vkqaFOkPWhrvvD3rUwOEk6Ct7DR7XgNSUCYnv2F-P8HjsHdruvvkr4HXPUmFDMFWQvAlvzbTsosJ3BK-5ycTLA68svnRJbRr8CFaQwYk0_iuzvE7nqOZv8ImHIsN5IHe5k9NCiZMupf7EeXIqY6Y13jA"
        // }

            console.log(user);
        

            

              // let myVar = setTimeout(()=>{  }, 2000);
                 
                   
               this.Testlogin(user, options)

                   
                    

                        // {
                        //   "provider":user.provider,
                        //   "idToken":user.idToken
                        // }
               
            

        //const credentials = JSON.stringify({ "provider": externalAuth.provider, "idToken": externalAuth.idToken })

        



          //this.authenticationService.validateExternalAuth(externalAuth);

          // this.authenticationService.validateExternalAuth(externalAuth)
          // .subscribe((res)=>
          // {
          //   localStorage.setItem("token",res.token);
          //   this.notification.showSuccess("Google login Sucessfully","Success");
          //   console.log("Google signin successfully");
          //   this.router.navigate(["/admin"]);
          // },
          // error =>
          // {
          //   console.log("Error response: " + error);
          // });
      });
  }




  Testlogin(user:SocialUser, options)
  {

    const externalAuth1 = {
            provider:user.provider,
            idToken:user.idToken
          };

          console.log(externalAuth1);

    this.http.post('http://localhost:19158/api/v1/GoogleAuth/ExternalLogin',externalAuth1 
    ,options )
              .subscribe((res)=>
              {
                const token = (<any>res).token;
                console.log("=======================================");
                console.log("The output: "+token);
              }
              );  
  }

    externalFacebookLogin()
    {
      console.log('facebook login was clicked....');
      this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID)
      .then(res =>{
        const user:SocialUser={...res};
        console.log(user);

        const externalAuth: ExternalAuthDto = {
          provider: user.provider,
          idToken: user.authToken
        }

        console.log(externalAuth.idToken  + "\n" + externalAuth.provider);

      });
    }

  }









    // private validateExternalAuth(externalAuth: ExternalAuthDto) {
    //   this.http.post<ExternalAuthDto>(`${environment.apiURL}`+'/api/v1/googleauth/externallogin', externalAuth)
    //     .subscribe(res => {
    //       localStorage.setItem("token", res.idToken);
    //       this.notification.showSuccess("sucess","Google Sign Successfully");
    //       console.log("google login success");
    //     },
    //     error => {

    //      console.log("Error response: " + error);
    //     });
    // }











