import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import {Route, Router} from '@angular/router';
import {NgForm}  from '@angular/forms';
import {CustomValidationService} from './../../Services/custom-validation.service';
import {AuthenticationService} from './../../Services/authentication.service';
import {GoogleLoginProvider, SocialAuthService} from 'angularx-social-login';
import {ExternalAuthDto} from './../../Models/ExternalAuthDto'

// import { SocialAuthService } from "angularx-social-login";
import { SocialUser } from "angularx-social-login";
// import { GoogleLoginProvider } from "angularx-social-login";



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup;
  submitted = false;
  showError:boolean;
  
  public user: SocialUser = new SocialUser;
  constructor(private fb:FormBuilder, private customValidator:CustomValidationService,
     private http:HttpClient,
     private router:Router,private authenticationService:AuthenticationService,
     private socialAuthService: SocialAuthService
     ) {
   
  }

  ngOnInit(): void {
    
    this.loginForm=this.fb.group({
      name:[''],
      password:['',Validators.compose([Validators.required, this.customValidator.patternValidator()])],
      confirmPassword:['', [Validators.required]]
    },
    {
      validator: this.customValidator.MatchPassword('password', 'confirmPassword'),
    }
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
        console.log("confirm password : "+this.loginForm.value.confirmPassword);
        console.table(this.loginForm.value);

        const credentials = JSON.stringify({ "username": this.loginForm.value.name, "password": this.loginForm.value.password })
       
        this.authenticationService.getWebToken(credentials);
      }

     
    }


    externalLogin()
    {
      console.log("External login was called ......");

      //this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);

      this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID)

      .then(res =>{
          const user:SocialUser={...res};
          console.log(user);

          const externalAuth: ExternalAuthDto = {
            provider: user.provider,
            idToken: user.idToken
          }

          console.log('idToken: ' + user.idToken);

      },error =>{
            console.log(error);
      });
 
    }

    
    // private validateExternalAuth(externalAuth: ExternalAuthDto) {
    //   this._authService.externalLogin('api/v1/GoogleAuth/externallogin', externalAuth)
    //     .subscribe(res => {
    //       localStorage.setItem("token", res.token);
    //       this._authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
    //       this._router.navigate([this._returnUrl]);
    //     },
    //     error => {
    //       this.errorMessage = error;
    //       this.showError = true;
    //       this._authService.signOutExternal();
    //     });
    // }




  }
  





