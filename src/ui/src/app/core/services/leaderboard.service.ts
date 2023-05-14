import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { LeaderboardModel } from '../interfaces/leaderboard-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  constructor(private apiService : ApiService) { }

  getLeaderboards(): Observable<LeaderboardModel> {
    return this.apiService.get<LeaderboardModel>('/information/leaderboards');
  }
}
