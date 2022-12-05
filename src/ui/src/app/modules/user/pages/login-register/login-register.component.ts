import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit {

  hide = true;
  repeatedHide = true;
  access: FormGroup;

  constructor(public formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.access = this.formBuilder.group({
      username: ["", Validators.required],
      email: ["", Validators.required],
      password: ["", Validators.required],
      repeatPassword: ["", Validators.required],
      login: true,
    })
  }

  log(){
    console.log(this.access)
  }

  disable(){
    if(!this.access.get("login").value){
      this.access.get("email").disable()
      this.access.get("repeatPassword").disable()
    }else{
      this.access.get("email").enable()
      this.access.get("repeatPassword").enable()
    }

  }

}
