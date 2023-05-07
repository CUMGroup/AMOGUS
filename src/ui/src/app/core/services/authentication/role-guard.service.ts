import { Injectable } from '@angular/core';
import { AuthGuardService } from './auth-guard.service';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate{

  constructor(private authService: AuthenticationService, private authGuard: AuthGuardService, private router: Router) { }


  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    let url = state.url;
    if (url == '/user/teacher-view') {
      if (!this.authService.hasRole('Teacher') || !this.authService.hasRole('Admin')) {
        this.router.navigate(['/']);
        alert('You don\'t have permission to access this page!');
        return false;
      }
      return true;
    }
    if (!this.authService.hasRole('User')) {
      this.router.navigate(['/user/login']);
      return false;
    }
    return true;
  }

}
