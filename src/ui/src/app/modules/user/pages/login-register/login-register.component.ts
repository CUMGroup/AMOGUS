import {AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../../../../core/services/authentication/authentication.service";
import {Subscription, catchError, throwError} from "rxjs";
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login-register',
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent implements OnInit, OnDestroy {

  hide = true;
  registerToggle = false;
  repeatedHide = true;
  access: FormGroup;
  message: string;
  signSubscription: Subscription;

  @ViewChild('errorPopup') errorPopup! : ElementRef;

  constructor(public formBuilder: FormBuilder, public authService: AuthenticationService, private router : Router) {
  }

  ngOnInit(): void {
    this.access = this.formBuilder.group({
      username: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)]],
      repeatPassword: ["", [Validators.required, Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$/)]],
      login: true,
    })
  }

  ngOnDestroy(): void {
    this.signSubscription?.unsubscribe()
  }

  login() {
    if(this.registerToggle) {
      if(this.access.get("password").invalid) {
        this.message = "Password must have...<ul><li>at least one uppercase character</li><li>at least one lowercase character</li><li>at least one digit</li><li>at least one special character</li><li>at least 6 characters long</li></ul>"
        return;
      }
      if(this.access.get("repeatPassword").invalid) {
        this.message = "Passwords must match";
      }
      if(this.access.invalid) {
        return;
      }
    }else {
      if(this.access.get("email").invalid) {
        this.message = "Enter a valid Email";
        return;
      }
      if(this.access.get("password").invalid) {
        this.message = "Password must have...<ul><li>at least one uppercase character</li><li>at least one lowercase character</li><li>at least one digit</li><li>at least one special character</li><li>at least 6 characters long</li></ul>"
        return;
      }
    }

    if (this.access.get("password").value !== this.access.get("repeatPassword").value && this.registerToggle) {
      this.message = "Passwords must match";
      this.access.get("repeatPassword").setErrors({error: "Passwords must match"});
      return;
    } else {
      this.message = "";
    }
    if (!this.registerToggle) {
      this.signSubscription = this.authService.login(
        this.access.get("email").value,
        this.access.get("password").value
      )
      .pipe(catchError(e => this.handleError(e)))
      .subscribe(e => this.navigateToHome(false))
    } else {
      this.signSubscription = this.authService.register(
        this.access.get("email").value,
        this.access.get("username").value,
        this.access.get("password").value
      )
      .pipe(catchError(e => this.handleError(e)))
      .subscribe(e => this.navigateToHome(false))
    }
  }

  private handleError(error: HttpErrorResponse) {
    if(error.status === 401 || error.status === 422) {
      this.errorPopup.nativeElement.style.opacity='100';
      this.errorPopup.nativeElement.getElementsByClassName('error-description')[0].innerText = error.error.message;
      setTimeout(() => this.errorPopup.nativeElement.style.opacity='0', 5000);
    }
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }

  navigateToHome(keepState = true) {
    if(keepState)
      this.router.navigateByUrl('');
    else
      window.location.href = '';
  }


  disable() {
    this.registerToggle = !this.registerToggle;
    if (!this.registerToggle) {
      this.access.get("username").disable()
      this.access.get("repeatPassword").disable()
    } else {
      this.access.get("username").enable()
      this.access.get("repeatPassword").enable()
    }

  }

}
