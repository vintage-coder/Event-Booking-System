import { Component, OnInit } from '@angular/core';
import {CustomValidationService} from './../../Services/custom-validation.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;

  User: any = ['Super Admin', 'Author', 'Reader'];
  constructor(private fb:FormBuilder, private customValidator:CustomValidationService) { }

  ngOnInit(): void {
    this.registerForm=this.fb.group({
        name:[''],
        email:[''],
        username:[''],
        password:[''],
        confirmPassword:['']
    });
  }


  onRegister()  
  {
    console.log("Register was called...");
    console.log("name :"+this.registerForm.value.name);
    console.log("email :"+this.registerForm.value.email);
    console.table(this.registerForm.value);
  }
}
