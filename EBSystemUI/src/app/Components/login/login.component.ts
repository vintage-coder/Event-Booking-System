import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import {Route, Router} from '@angular/router';
import {NgForm}  from '@angular/forms';
import {CustomValidationService} from './../../Services/custom-validation.service';
import {AuthenticationService} from './../../Services/authentication.service';




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm:FormGroup;
  submitted = false;
  showError:boolean;
  

  constructor(private fb:FormBuilder, private customValidator:CustomValidationService,
     private http:HttpClient,
     private router:Router,private authenticationService:AuthenticationService) {
   
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
 
    }


  }
  





