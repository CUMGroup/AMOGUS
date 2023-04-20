import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserStats } from '../interfaces/user-stats';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  constructor(private apiService : ApiService) { }

  getUserStats(): Observable<UserStats> {
    return this.apiService.get<UserStats>('/stats');
  }
}
