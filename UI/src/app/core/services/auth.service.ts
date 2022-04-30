import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'

export const TOKEN_NAME: string = 'access_token'

@Injectable()
export class AuthService {
  constructor(private httpClient: HttpClient) {}

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
    // TODO authentication
  }
}
