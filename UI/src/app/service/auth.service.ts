import { HttpClient } from '@angular/common/http'
import { Auth } from '../model/auth.model'
import { Router } from '@angular/router'
import { Injectable } from '@angular/core'
import { environment } from '../../environments/environment'

export const TOKEN_NAME: string = 'access_token'

@Injectable()
export class AuthService {
  constructor(private httpClient: HttpClient, private router: Router) {}

  getToken(): string {
    return localStorage.getItem(TOKEN_NAME)
  }

  setToken(token: string): void {
    localStorage.setItem(TOKEN_NAME, token)
  }

  isTokenExpired(token?: string): boolean {
    if (!token) token = this.getToken()
    if (!token) return true
  }

  async requestToken(username: string) {
    const apiURL = `${environment.baseApiUrl}/user/authenticate/${username}`

    this.httpClient
      .get<Auth>(apiURL)
      .toPromise()
      .then((res) => {
        this.setToken(res.token)
        this.router.navigate(['/chat'])
      })
  }
}
