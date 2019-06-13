import { Injectable } from '@angular/core';
import {
  CanActivate, Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivateChild,
} from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardUser implements CanActivate, CanActivateChild {
    constructor(private router: Router) { }
  
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {    
      if (localStorage.role === 'AppUser' || localStorage.role === 'undefined') {
        return true;
      }
      // not logged in so redirect to login page
      else {
        console.error("Can't access, not regular user");
        this.router.navigate(['/']);
        return false;
      }
    }
  
    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      return this.canActivate(route, state);
    }
  
  }
