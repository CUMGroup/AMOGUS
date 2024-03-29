import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiService } from '../api.service';

import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { LoginResult } from '../../interfaces/loginResult';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private api: ApiService) { }

  login(email: string, password: string): Observable<LoginResult> {
    return this.api.post<LoginResult>('/auth/login', { email:email, password:password }).pipe(
      tap(e => this.setSession(e)),
      catchError(this.handleError)
    );
  }

  register(email: string, username: string, password: string): Observable<LoginResult> {
    return this.api.post<LoginResult>('/auth/register', { email, username, password }).pipe(
      tap(e => this.setSession(e)),
      catchError(this.handleError)
    )
  }

  logout() {
    localStorage.removeItem("id_token");
  }

  isLoggedOut(): boolean {
    return !this.isLoggedIn();
  }

  private setSession(loginResult: LoginResult): void {
    localStorage.setItem('id_token', loginResult.token);
  }

  /**
   * @deprecated Use isAuthenticated instead!
   */
  public isLoggedIn(): boolean {
    return this.isAuthenticated();
  }

  public getAuthToken(): string{
    return localStorage.getItem('id_token');
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    }else if(error.status === 401 || error.status === 422) {
      throw error
    }else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
  //nobodys gonna know... ;-) @AlexMi-Ha

  public getRoles() {
    const token = this.getAuthToken();
    if (token == null) {
      return [];
    }
    let encToken = jwtDecode(token);
    return encToken['roles'];
  }

  public isAuthenticated() {
    const token = this.getAuthToken();
    if (token == null) {
      return false;
    }
    let encToken = jwtDecode(token);
    if(+encToken["exp"] * 1000 < new Date().getTime()) {
      this.logout();
      return false;
    }

    const roles = this.getRoles();
    if (roles.includes('User')) {
      return true;
    }
    return false;
  }

  public hasRole(role: string) {
    if (this.getRoles().includes(role)) {
      return true;
    }
    return false;
  }

  public isTeacherOrAdmin(): boolean {
    if (this.hasRole('Moderator') || this.hasRole('Admin')){
      return true;
    }
    return false;
  }

}


