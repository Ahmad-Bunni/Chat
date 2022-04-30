import { Component } from '@angular/core'
import { AuthService } from '../../../../core/services/auth.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  username: string

  constructor(private authService: AuthService) {}

  async login() {
    await this.authService.requestToken(this.username)
  }
}
