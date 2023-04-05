import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl = '/api';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(new URL(endpoint, this.baseUrl).href);
  }

  post<T>(endpoint: string, object: any): Observable<T> {
    return this.http.post<T>(this.baseUrl+endpoint, object);
  }

  put<T>(endpoint: string, object: any): Observable<T> {
    return this.http.put<T>(new URL(endpoint, this.baseUrl).href, object);
  }

  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(new URL(endpoint, this.baseUrl).href);
  }

}
