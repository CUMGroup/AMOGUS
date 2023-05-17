import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { ProfileModel } from '../interfaces/profile-model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private apiService : ApiService) { }

  getProfile(): Observable<ProfileModel> {
    return this.apiService.get<ProfileModel>('/user/profile');
  }
}
