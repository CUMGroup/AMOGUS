import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthenticationService, private router: Router) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
      if (this.authService.isAuthenticated()) {
        return true;
      }
      this.router.navigate(['/user/login']);
      alert('You are not logged in yet.');
      return false;
  }
}
