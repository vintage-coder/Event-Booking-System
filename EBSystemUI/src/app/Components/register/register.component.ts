import { Component, OnInit } from '@angular/core';
import {CustomValidationService} from './../../Services/custom-validation.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { AuthenticationService } from 'src/app/Services/Authentication/authentication.service';
import { UserPageComponent } from '../user-page/user-page.component';
import { UserRegistrationDto } from 'src/app/Dtos/UserRegistrationDto';
import {NotificationService} from './../../Services/notification.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;

  User: any = ['Super Admin', 'Author', 'Reader'];
  constructor(private fb:FormBuilder,
    private customValidator:CustomValidationService,
    private authService:AuthenticationService,
    private notifyService:NotificationService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.registerForm=this.fb.group({
        name:[''],
        email:[''],
        username:[''],
        password:[''],
        confirmPassword:['']
    });
  }


  Register()  
  {
    console.log("Register was called...");

    //this.notifyService.showSuccess("Data shown successfully !!", "Notification")
    
    this.toastr.success("Hello, I'm the toastr message.");

    
    console.log("name :"+this.registerForm.value.name);
    console.log("email :"+this.registerForm.value.email);
    console.table(this.registerForm.value);

    // const credentials:UserRegistrationDto = JSON.stringify(
    //   { "username": this.registerForm.value.username,
    //     "email":this.registerForm.value.email,
    //    "password": this.registerForm.value.password,
    //    "confirmPassword":this.registerForm.value.confirmPassword 
    //   });

    const credentials:UserRegistrationDto = {
      UserName:this.registerForm.value.username,
      Email:this.registerForm.value.email,
      Password:this.registerForm.value.password,
      ConfirmPassword:this.registerForm.value.confirmPassword
      };

    this.authService.registerUser(credentials);
  }
}
