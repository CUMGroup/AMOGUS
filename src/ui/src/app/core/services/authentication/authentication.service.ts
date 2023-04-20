import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiService } from '../api.service';

import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { User } from '../../interfaces/user';
import { LoginResult } from '../../interfaces/loginResult';

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
    return this.api.post<LoginResult>('/auth/register', JSON.stringify({ email, username, password })).pipe(
      tap(e => this.setSession),
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

  public isLoggedIn(): boolean {
    //double negation for type convertion
    return !!localStorage.getItem('id_token');
  }

  public getAuthToken(): string{
    return localStorage.getItem('id_token');
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
  //nobodys gonna know... ;-) @AlexMi-Ha
}


