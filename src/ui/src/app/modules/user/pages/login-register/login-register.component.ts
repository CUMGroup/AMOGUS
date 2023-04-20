import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../../../../core/services/authentication/authentication.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit, OnDestroy {

  hide = true;
  repeatedHide = true;
  access: FormGroup;
  message: string;
  signSubscription: Subscription;

  constructor(public formBuilder: FormBuilder, public authService: AuthenticationService) {
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

  ngOnDestroy(): void {
    this.signSubscription.unsubscribe()
  }

  login() {
    if (this.access.get("password").value !== this.access.get("repeatPassword").value &&
      !this.access.get("login").value) {
      this.message = "enter the same password"
    } else {
      this.message = "";
    }
    if (this.access.get("login").value) {
      this.signSubscription = this.authService.login(
        this.access.get("email").value,
        this.access.get("password").value
      ).subscribe()
    } else {
      this.signSubscription = this.authService.register(
        this.access.get("email").value,
        this.access.get("username").value,
        this.access.get("password").value
      ).subscribe()
    }
  }

  disable() {
    if (!this.access.get("login").value) {
      this.access.get("username").disable()
      this.access.get("repeatPassword").disable()
    } else {
      this.access.get("username").enable()
      this.access.get("repeatPassword").enable()
    }

  }

}
