import { Injectable } from '@angular/core'
import { Router, CanActivate } from '@angular/router'
import { AuthenticationService } from '@core/services'

@Injectable()
export class AuthenticationGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) {}

  canActivate() {
    if (!this.authService.isTokenExpired()) {
      return true
    }

    this.router.navigate(['/login']) // TODO ngrx or ngxs
    return false
  }
}
