import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../../interfaces/user';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: ApiService) { }

  getUser(): Observable<User> {
    return this.apiService.get<User>("/user/profile");
  }

  deleteUser(): Observable<User> {
    return this.apiService.delete<User>("/user/profile");
  }
}
