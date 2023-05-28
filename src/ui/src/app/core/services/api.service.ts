import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { environment } from './../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl = environment.API_BASE_URL;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(this.buildUrl(endpoint));
  }

  postPlainText(endpoint: string, object: any): Observable<string> {
    return this.http.post(this.buildUrl(endpoint), object, { responseType: 'text' });
  }

  post<T>(endpoint: string, object: any): Observable<T> {
    return this.http.post<T>(this.buildUrl(endpoint), object);
  }

  put<T>(endpoint: string, object: any): Observable<T> {
    return this.http.put<T>(this.buildUrl(endpoint), object);
  }

  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(this.buildUrl(endpoint));
  }

  private buildUrl(endpoint : string) : string {
    if(endpoint.charAt(0) == '/')
      endpoint = endpoint.substring(1);

    return new URL('/api/' + endpoint, this.baseUrl).href;
  }
}
